using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

/// <summary>
/// This class was taken in its entirety from stack overflow => 
/// https://stackoverflow.com/questions/34638823/store-complex-object-in-tempdata
/// </summary>
public static class TempDataExtensions
{
    public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
    {
        tempData[key] = JsonConvert.SerializeObject(value);
    }

    public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
    {
        object o;
        tempData.TryGetValue(key, out o);
        return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
    }
}