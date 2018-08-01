using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.IO;

using Syncfusion.DocIO.DLS;

using Xamarin.Forms;

namespace DocIOSample
{
    public class SampleViewModel
    {
        public ICommand GeracaoArquivoWord { get; set; }

        public SampleViewModel()
        {
            GeracaoArquivoWord = new Command(GerarArquivoWord);
        }

        async void GerarArquivoWord()
        {
            WordDocument localdocumento = new WordDocument();

            IWSection localSection = localdocumento.AddSection();

            IWParagraph localparagraph = localSection.AddParagraph();

            //CachedImage localimagem = new CachedImage();
            //localimagem.Source = "ESFJPDocumento";
            //
            //var localbyte = await localimagem.GetImageAsJpgAsync();
            //IWPicture localpicture = localparagraph.AppendPicture(localbyte);

            var auxiliartexto = "Geral;\r\nProjetos;\r\nMarketing;\r\nPessoas;";

            IWTextRange localtexto = localparagraph.AppendText(auxiliartexto);
            localtexto.CharacterFormat.FontSize = 14;
            localtexto.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Black;

            MemoryStream arquivostream = new MemoryStream();
            arquivostream.Position = 0;

            localdocumento.Save(arquivostream, Syncfusion.DocIO.FormatType.Docx);

            localdocumento.Close();

            await DependencyService.Get<ISave>().Save("TesteESFJPDocumento.docx", "application/msword", arquivostream);

            //Stream docStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "TesteESFJPDocumento.docx");

            //localdocumento.Open(docStream, Syncfusion.DocIO.FormatType.Docx);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
