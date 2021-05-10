using CsvHelper;
using CsvHelper.Configuration;
using MultiLanguageSupporter.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace MultiLanguageSupporter
{
    public class CSVLoader
    {
        private const string RESOURCE_FOLDER_NAME = "MultiLanguageSupporter";
        private const string SETTINGS_FILE_NAME = "MLSSettings";
        private const string LOCAL_FOLDER_PATH = @"Assets\Resources";
        private const string DEFAULT_LANGUAGE = "eng";

        private static string currentResourceFolder = $@"{LOCAL_FOLDER_PATH}\{RESOURCE_FOLDER_NAME}";
        private static string currentDefaultLanguage = DEFAULT_LANGUAGE;

        public static void LoadSettings()
        {
            var settings = Resources.Load<MultiLanguageSupporterSettings>(@$"{RESOURCE_FOLDER_NAME}\{SETTINGS_FILE_NAME}");
            if (settings != null)
            {
                currentResourceFolder = settings.pathSettings.Path;
                currentDefaultLanguage = settings.defaultLanguageISOThreeLetter;
            }
            else
                Debug.LogWarning($@"SO with setting in {LOCAL_FOLDER_PATH}\{SETTINGS_FILE_NAME} not found! " +
                            "Load deafult settings:\n" +
                            $"Folder with CSVs: {LOCAL_FOLDER_PATH}\n" +
                            $"Default language: {DEFAULT_LANGUAGE}\n");
        }

        public static void Load(ref Dictionary<string, string> keyValueData, string threeLetterISOLangName)
        {
            LoadSettings();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            if (!Directory.Exists(currentResourceFolder))
            {
                Directory.CreateDirectory(currentResourceFolder);
                Debug.LogWarning($"Directory {currentResourceFolder} was not founded! Directory created.");
            }

            try
            {
                using (var reader = LoadReader(config, threeLetterISOLangName))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<DataStructureMap>();
                    var records = csv.GetRecords<DataStructure>();
                    foreach (var record in records)
                        keyValueData.Add(record.Key, record.Value);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Cant load the lang file! Because: {e}");
            }
        }

        private static StreamReader LoadReader(CsvConfiguration config, string threeLetterISOLangName)
        {
            var names = Directory.GetFiles($@"{currentResourceFolder}", $"{threeLetterISOLangName}.csv");
            if (names.Length != 0) return new StreamReader(names[0]);
            Debug.LogWarning($"File {threeLetterISOLangName}.csv dont exist!" +
                $" Try load default {currentDefaultLanguage}.csv");

            names = Directory.GetFiles($@"{currentResourceFolder}", $"{currentDefaultLanguage}.csv");
            if (names.Length != 0) return new StreamReader(names[0]);

            return CreateDefaultCSV(currentResourceFolder, config);
        }

        private static StreamReader CreateDefaultCSV(string folder, CsvConfiguration config)
        {
            var defaultRecords = new List<DataStructure>() {
                new DataStructure() { Key = "KEY_1", Value = "VALUE_1" },
                new DataStructure() { Key = "KEY_2", Value = "VALUE_2" },
                new DataStructure() { Key = "KEY_3", Value = "VALUE_3" },
            };

            string newFullpath = $@"{folder}\{DEFAULT_LANGUAGE}.csv";

            try
            {
                using (FileStream fs = File.Create(newFullpath))
                using (var writer = new StreamWriter(fs))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.Context.RegisterClassMap<DataStructureMap>();
                    csv.WriteRecords(defaultRecords);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            Debug.LogWarning($"File {newFullpath} was not founded! Created default one.");

            return new StreamReader(newFullpath);
        }
    }
}