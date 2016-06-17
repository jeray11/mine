using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Plugins
{
   public class PluginFileParser
    {
       public static IList<string> ParseInstalledPluginsFile(string filePath)
       {
           //read and parse the file
           if (!File.Exists(filePath))
               return new List<string>();

           var text = File.ReadAllText(filePath);
           if (String.IsNullOrEmpty(text))
               return new List<string>();

           //Old way of file reading. This leads to unexpected behavior when a user's FTP program transfers these files as ASCII (\r\n becomes \n).
           //var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

           var lines = new List<string>();
           using (var reader = new StringReader(text))
           {
               string str;
               while ((str = reader.ReadLine()) != null)
               {
                   if (String.IsNullOrWhiteSpace(str))
                       continue;
                   lines.Add(str.Trim());
               }
           }
           return lines;
       }

       public static PluginDescriptor ParsePluginDescriptionFile(string filePath)
       {
           var descriptor = new PluginDescriptor();
           var text = File.ReadAllText(filePath);
           if (String.IsNullOrEmpty(text))
               return descriptor;

           var settings = new List<string>();
           using (var reader = new StringReader(text))
           {
               string str;
               while ((str = reader.ReadLine()) != null)
               {
                   if (String.IsNullOrWhiteSpace(str))
                       continue;
                   settings.Add(str.Trim());
               }
           }

           //Old way of file reading. This leads to unexpected behavior when a user's FTP program transfers these files as ASCII (\r\n becomes \n).
           //var settings = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

           foreach (var setting in settings)
           {
               var separatorIndex = setting.IndexOf(':');
               if (separatorIndex == -1)
               {
                   continue;
               }
               string key = setting.Substring(0, separatorIndex).Trim();
               string value = setting.Substring(separatorIndex + 1).Trim();

               switch (key)
               {
                   case "Group":
                       descriptor.Group = value;
                       break;
                   case "FriendlyName":
                       descriptor.FriendlyName = value;
                       break;
                   case "SystemName":
                       descriptor.SystemName = value;
                       break;
                   case "Version":
                       descriptor.Version = value;
                       break;
                   case "SupportedVersions":
                       {
                           //parse supported versions
                           descriptor.SupportedVersions = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(x => x.Trim())
                               .ToList();
                       }
                       break;
                   case "Author":
                       descriptor.Author = value;
                       break;
                   case "DisplayOrder":
                       {
                           int displayOrder;
                           int.TryParse(value, out displayOrder);
                           descriptor.DisplayOrder = displayOrder;
                       }
                       break;
                   case "FileName":
                       descriptor.PluginFileName = value;
                       break;
                   case "LimitedToStores":
                       {
                           //parse list of store IDs
                           foreach (var str1 in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                     .Select(x => x.Trim()))
                           {
                               int storeId;
                               if (int.TryParse(str1, out storeId))
                               {
                                   descriptor.LimitedToStores.Add(storeId);
                               }
                           }
                       }
                       break;
                   default:
                       break;
               }
           }

           //nopCommerce 2.00 didn't have 'SupportedVersions' parameter
           //so let's set it to "2.00"
           if (descriptor.SupportedVersions.Count == 0)
               descriptor.SupportedVersions.Add("2.00");

           return descriptor;
       }
    }
}
