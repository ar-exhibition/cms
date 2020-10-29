using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vlingo.UUID;
using Net.Codecrete.QrCodeGenerator;
using System.IO;
using System.Drawing.Imaging;

namespace cms.ar.xarchitecture.de.Helper
{
    public static class MarkerCreator
    {
        public static string createQRCode(string sceneName, string host)
        {
            NameBasedGenerator uuidCreator = new NameBasedGenerator(HashType.SHA1);
            ImageFormat imageFormat = ImageFormat.Png;

            string markerUUID = uuidCreator.GenerateGuid(sceneName + DateTime.Now).ToString();
            string url = host + "/QRCode/Open?uuid=";
            string content = url + markerUUID;
            QrCode qr = QrCode.EncodeText(content, QrCode.Ecc.Medium);

            string dir = Directory.GetCurrentDirectory();
            string filename = markerUUID + "." + imageFormat.ToString();

            var path = Path.Combine(
                dir, "static", "content", "marker",
                filename);

            using (var stream = new FileStream(path, FileMode.Create))
            using (var bitmap = qr.ToBitmap(40, 1))
            {
                bitmap.Save(stream, imageFormat);
            }

            return filename;
        }
    }
}
