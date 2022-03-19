using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using Domain;
using System.Collections;

namespace Portal.Utility {
    public static class ExtentionMethods {
        public static string ToBase64String(this IFormFile file) {
            if (file != null && file.Length > 0) {
                using var ms = new MemoryStream(); var image = Image.Load(file.OpenReadStream());
                image.SaveAsJpeg(ms);
                var fileBytes = ms.ToArray();
                return Convert.ToBase64String(fileBytes);
            } else {
                return null;
            }
        }
    }
}
