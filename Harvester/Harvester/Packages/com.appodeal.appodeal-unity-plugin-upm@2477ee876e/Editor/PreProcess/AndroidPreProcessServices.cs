using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using AppodealStack.UnityEditor.Utils;
using AppodealStack.UnityEditor.InternalResources;

// ReSharper Disable CheckNamespace
namespace AppodealStack.UnityEditor.PreProcess
{
    public static class AndroidPreProcessServices
    {
    
    #region Firebase

        public static void GenerateXMLForFirebase()
        {
            string xmlFilePath = Path.Combine(Application.dataPath,
                                              AppodealEditorConstants.AppodealAndroidLibPath,
                                              AppodealEditorConstants.FirebaseAndroidConfigPath,
                                              AppodealEditorConstants.FirebaseAndroidConfigFile);

            if (!AppodealSettings.Instance.FirebaseAutoConfiguration)
            {
                RemoveFirebaseXml(xmlFilePath);
                return;
            }

            string jsonFilePath = Path.Combine(Application.dataPath, AppodealEditorConstants.FirebaseAndroidJsonFile);

            if (!File.Exists(jsonFilePath))
            {
                Debug.LogError($"Firebase Android Configuration file was not found at {jsonFilePath}\nThis service cannot be configured correctly.");
                return;
            }

            var firebaseStrings = ParseFirebaseJson(jsonFilePath);

            if (firebaseStrings == null)
            {
                Debug.LogError($"Couldn't find a valid Firebase configuration for package name: {Application.identifier} in {jsonFilePath} file.\nThis service cannot be configured correctly.");
                return;
            }

            CreateOrReplaceFirebaseXml(xmlFilePath, firebaseStrings);
        }

        private static Dictionary<string,string> ParseFirebaseJson(string path)
        {
            string jsonString = new StreamReader(path).ReadToEnd();
            var model = JsonUtility.FromJson<FirebaseJsonModel>(jsonString);

            if ((model?.client?.Count ?? 0) <= 0) return null;

            foreach (var client in model.client)
            {
                var clientInfo = client.client_info;
                var androidClientInfo = clientInfo.android_client_info;

                if (androidClientInfo?.package_name != Application.identifier) continue;
                
                var projectInfo = model.project_info;
                return new Dictionary<string, string>
                {
                    {"firebase_database_url", projectInfo?.firebase_url},
                    {"gcm_defaultSenderId", projectInfo?.project_number},
                    {"google_storage_bucket", projectInfo?.storage_bucket},
                    {"project_id", projectInfo?.project_id},
                    {"google_api_key", client.api_key?[0].current_key},
                    {"google_crash_reporting_api_key", client.api_key?[0].current_key},
                    {"google_app_id", clientInfo.mobilesdk_app_id},
                    {"default_web_client_id", client.oauth_client?[0].client_id}
                };
            }
            return null;
        }

        private static void CreateOrReplaceFirebaseXml(string path, Dictionary<string,string> firebaseStrings)
        {
            var xmlDocument = new XmlDocument();
            var root = xmlDocument.DocumentElement;

            var xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDocument.InsertBefore(xmlDeclaration, root);

            var resourcesElement = xmlDocument.CreateElement("resources");

            var attribute = xmlDocument.CreateAttribute("tools", "keep", "http://schemas.android.com/tools");
            string toolsKeep = "@string/firebase_database_url,@string/gcm_defaultSenderId,@string/google_storage_bucket,@string/project_id,@string/google_api_key,@string/google_crash_reporting_api_key,@string/google_app_id,@string/default_web_client_id";
            attribute.Value = toolsKeep;
            resourcesElement.Attributes.Append(attribute);

            firebaseStrings.Keys.ToList().ForEach(key =>
                resourcesElement.AppendChild(CreateFirebaseStringElement(key, firebaseStrings[key], xmlDocument)));

            xmlDocument.AppendChild(resourcesElement);
            xmlDocument.Save(path);
        }

        private static XmlElement CreateFirebaseStringElement(string elName, string elValue, XmlDocument xmlDocument)
        {
            var xmlElement = xmlDocument.CreateElement("string");
            xmlElement.SetAttribute("name", elName);
            xmlElement.SetAttribute("translatable", "false");
            var xmlText = xmlDocument.CreateTextNode(elValue);
            xmlElement.AppendChild(xmlText);
            
            return xmlElement;
        }

        private static void RemoveFirebaseXml(string path)
        {
            if (!File.Exists(path)) return;
            
            FileUtil.DeleteFileOrDirectory(path);
            FileUtil.DeleteFileOrDirectory($"{path}.meta");
        }
    
    #endregion

    #region Facebook
        public static void SetupManifestForFacebook()
        {
            string path = Path.Combine(Application.dataPath,
                                       AppodealEditorConstants.AppodealAndroidLibPath,
                                       AppodealEditorConstants.AndroidManifestFile);

            if (!File.Exists(path))
            {
                Debug.LogWarning($"Missing AndroidManifest.xml file at {path}." +
                                 "\nFacebook App ID cannot be added. This service won't be initialized properly!");
                return;
            }

            if (!AppodealSettings.Instance.FacebookAutoConfiguration)
            {
                return;
            }

            UpdateManifest(path);
        }

        private static void UpdateManifest(string fullPath)
        {
            string appId = AppodealSettings.Instance.FacebookAndroidAppId;
            string clientToken = AppodealSettings.Instance.FacebookAndroidClientToken;

            if (String.IsNullOrEmpty(appId) | String.IsNullOrEmpty(clientToken))
            {
                Debug.LogWarning("Facebook App ID / Client Token is empty (Appodeal > Appodeal Settings). This service won't be initialized properly!");
                return;
            }

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(fullPath);

            var xmlNode = FindChildNode(FindChildNode(xmlDocument, "manifest"), "application");
            if (xmlNode == null)
            {
                Debug.LogError("Error parsing " + fullPath);
                return;
            }
            
            string namespaceOfPrefix = xmlNode.GetNamespaceOfPrefix("android");
            var xmlElement = xmlDocument.CreateElement("meta-data");
            xmlElement.SetAttribute("name", namespaceOfPrefix, AppodealEditorConstants.FacebookApplicationId);
            xmlElement.SetAttribute("value", namespaceOfPrefix, "fb" + appId);
            SetOrReplaceXmlElement(xmlNode, xmlElement);

            var xmlElement2 = xmlDocument.CreateElement("meta-data");
            xmlElement2.SetAttribute("name", namespaceOfPrefix, AppodealEditorConstants.FacebookClientToken);
            xmlElement2.SetAttribute("value", namespaceOfPrefix, clientToken);
            SetOrReplaceXmlElement(xmlNode, xmlElement2);

            string value = AppodealSettings.Instance.FacebookAutoLogAppEvents.ToString().ToLower();
            var xmlElement3 = xmlDocument.CreateElement("meta-data");
            xmlElement3.SetAttribute("name", namespaceOfPrefix, AppodealEditorConstants.FacebookAutoLogAppEventsEnabled);
            xmlElement3.SetAttribute("value", namespaceOfPrefix, value);
            SetOrReplaceXmlElement(xmlNode, xmlElement3);

            string value2 = AppodealSettings.Instance.FacebookAdvertiserIDCollection.ToString().ToLower();
            var xmlElement4 = xmlDocument.CreateElement("meta-data");
            xmlElement4.SetAttribute("name", namespaceOfPrefix, AppodealEditorConstants.FacebookAdvertiserIDCollectionEnabled);
            xmlElement4.SetAttribute("value", namespaceOfPrefix, value2);
            SetOrReplaceXmlElement(xmlNode, xmlElement4);

            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            
            using(var w = XmlWriter.Create(fullPath, settings))
            {
                xmlDocument.Save(w);
            }
        }

        private static XmlNode FindChildNode(XmlNode parent, string name)
        {
            for (var xmlNode = parent.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
            {
                if (xmlNode.Name.Equals(name))
                {
                    return xmlNode;
                }
            }
            return null;
        }

        private static void SetOrReplaceXmlElement(XmlNode parent, XmlElement newElement)
        {
            string attribute = newElement.GetAttribute("name");
            string name = newElement.Name;
            if (TryFindElementWithAndroidName(parent, attribute, out XmlElement element, name))
            {
                parent.ReplaceChild(newElement, element);
            }
            else
            {
                parent.AppendChild(newElement);
            }
        }

        private static bool TryFindElementWithAndroidName(XmlNode parent, string attrNameValue, out XmlElement element, string elementType = "activity")
        {
            string namespaceOfPrefix = parent.GetNamespaceOfPrefix("android");
            for (var xmlNode = parent.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
            {
                var xmlElement = xmlNode as XmlElement;
                if (xmlElement != null && xmlElement.Name == elementType && xmlElement.GetAttribute("name", namespaceOfPrefix) == attrNameValue)
                {
                    element = xmlElement;
                    return true;
                }
            }
            element = null;
            return false;
        }

    #endregion

    }
}
