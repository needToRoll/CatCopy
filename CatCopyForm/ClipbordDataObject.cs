using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Windows.Forms;

namespace CatCopyForm
{
    public class ClipbordDataObject
    {
        public String Format { get; set; }
        public object Data { get; set; }
        public Type Type { get; set; }

        public ClipbordDataObject(String format, object data, Type type)
        {
            Format = format;
            Data = data;
            Type = type;
        }

        public ClipbordDataObject()
        {
        }

        public static ClipbordDataObject GenerateFromClipboard()
        {
            var result = new ClipbordDataObject();
            if (Clipboard.ContainsAudio())
            {
                result.Data = Clipboard.GetAudioStream();
                result.Format = DataFormats.WaveAudio;
            }
            else if (Clipboard.ContainsImage())
            {
                result.Data = Clipboard.GetImage();
                result.Format = DataFormats.Bitmap;
            }
            else if (Clipboard.ContainsFileDropList())
            {
                result.Data = Clipboard.GetFileDropList();
                result.Format = DataFormats.FileDrop;
            }
            else if (Clipboard.ContainsText())
            {
                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                    result.Data = Clipboard.GetText(TextDataFormat.Text);
                    result.Format = DataFormats.Text;
                }
                else if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                {
                    result.Data = Clipboard.GetText(TextDataFormat.UnicodeText);
                    result.Format = DataFormats.UnicodeText;
                }
                else if (Clipboard.ContainsText(TextDataFormat.Rtf))
                {
                    result.Data = Clipboard.GetText(TextDataFormat.Rtf);
                    result.Format = DataFormats.Rtf;
                }
                else if (Clipboard.ContainsText(TextDataFormat.Html))
                {
                    result.Data = Clipboard.GetText(TextDataFormat.Html);
                    result.Format = DataFormats.Html;
                }
                else
                {
                    result.Data = Clipboard.GetText();
                    result.Format = DataFormats.Text;
                }
            }

            if (result.Data == null)
            {
                return null;
            }
            result.Type = result.Data.GetType();
            return result;
        }
    }
}