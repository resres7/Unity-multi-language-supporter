using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace MultyLanguageSupporter
{
    public class CSVLoader
    {
        private const string LOCAL_FOLDER_PATH = @"Assets\Resources\Languages";
        private const string DEFAULT_LANGUAGE = "eng";

        public static void Load(ref Dictionary<string, string> keyValueData, string threeLetterISOLangName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            if (!Directory.Exists(LOCAL_FOLDER_PATH))
            {
                Directory.CreateDirectory(LOCAL_FOLDER_PATH);
                Debug.LogWarning($"Directory {LOCAL_FOLDER_PATH} was not founded! Directory created.");
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
            var names = Directory.GetFiles($@"{LOCAL_FOLDER_PATH}", $"{threeLetterISOLangName}.csv");
            if (names.Length != 0) return new StreamReader(names[0]);
            Debug.LogWarning($"File {threeLetterISOLangName}.csv dont exist! Try load default {DEFAULT_LANGUAGE}.csv");

            names = Directory.GetFiles($@"{LOCAL_FOLDER_PATH}", $"{DEFAULT_LANGUAGE}.csv");
            if (names.Length != 0) return new StreamReader(names[0]);

            return CreateDefaultCSV(LOCAL_FOLDER_PATH, config);
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