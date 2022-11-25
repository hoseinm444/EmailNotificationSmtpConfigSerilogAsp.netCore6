using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace SimpleEmailApp.ConfgureSetting
{
    public class WritableOptionsMail<T> : IWritableOptionsMail<T> where T : class, new()
    {
        //private properties
        private readonly IWebHostEnvironment _environment;
        private readonly IOptionsSnapshot<T> _options;
        private readonly IConfigurationRoot _configuration;
        private readonly string _section;
        private readonly string _file;
        //costructor 
        public WritableOptionsMail(IWebHostEnvironment environment, IOptionsSnapshot<T> options, IConfigurationRoot configuration, string section, string file)
        {
            _environment = environment;
            _options = options;
            _configuration = configuration;
            _section = section;
            _file = file;
        }
        //T value for generics type 
        public T Value => _options.Value;
        public T Get(string name) => _options.Get(name);

        public void Update(Action<T> applyChanges)
        {
            var fileProvider = _environment.ContentRootFileProvider;
            var fileInfo = fileProvider.GetFileInfo(_file);
            var physicalPath = fileInfo.PhysicalPath;
            var jObject = JsonSerializer.Deserialize<JsonObject>(File.ReadAllText(physicalPath)); //<JObject>(File.ReadAllText(physicalPath));

            if (jObject is null)
                return;

            var sectionObject = jObject.TryGetPropertyValue(_section, out JsonNode? section) ?
               JsonSerializer.Deserialize<T>(section?.ToString()) : (Value ?? new T());

            applyChanges(sectionObject);
            jObject[_section] = JsonObject.Parse(JsonSerializer.Serialize(sectionObject));
            File.WriteAllText(physicalPath, JsonSerializer.Serialize(jObject, new JsonSerializerOptions { WriteIndented = true }));
            _configuration.Reload();
        }
    }
}
