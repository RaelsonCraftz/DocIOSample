using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Java.IO;

using Xamarin.Forms;

using DocIOSample.Droid;

[assembly: Dependency(typeof(SaveAndroid))]
namespace DocIOSample.Droid
{
    public class SaveAndroid : ISave
    {

        public async Task Save(string fileName, String contentType, MemoryStream stream)
        {
            string exception = string.Empty;
            string root = null;
            //if (Android.OS.Environment.IsExternalStorageEmulated)
            //{
            //    root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            //}
            //else
            root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            Java.IO.File myDir = new Java.IO.File(root + "/Atas");
            myDir.Mkdir();
            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists()) file.Delete();

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());
                outs.Flush();
                outs.Close();
            }

            catch (Exception e)
            {
                exception = e.ToString();
            }
            finally
            {
                stream.Dispose();
            }

            if (file.Exists())
            {
                Android.Net.Uri path = Android.Net.Uri.FromFile(file);
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(path, mimeType);

                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
            }

        }

    }
}