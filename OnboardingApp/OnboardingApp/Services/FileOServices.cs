using Newtonsoft.Json;
using OnboardingApp.Models;
using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingApp.Services
{
    class FileOServices
    {
        public FileOServices()
        {

        }

        public BindableCollection<Employee> LoadText(string PATH)
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindableCollection<Employee>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindableCollection<Employee>>(fileText);
            }

        }

        public void SaveText(object todoEmployee, string PATH)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(todoEmployee);
                writer.WriteLine(output);
            }
        }

        internal void SaveData(List<Employee> employees, string path)
        {
            throw new NotImplementedException();
        }
    }
}
