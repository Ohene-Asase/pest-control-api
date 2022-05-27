using Humanizer;
using PestControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PestControl.Services
{
    public class EnumData
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public interface IEnumService
    {
        List<EnumData> GetList(string name);
    }

    public class EnumService : IEnumService
    {

        public List<EnumData> GetList(string name)
        {
            switch (name)
            {
                case "Gender":
                    return GetEnumValues<Gender>();
                default:
                    throw new ArgumentOutOfRangeException("Unknown Enum");

            }

        }


        private static List<EnumData> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(x => new EnumData
            {
                Value = x.ToString(),
                Label = x.ToString().Titleize()
            }).ToList();
        }

    }
}
