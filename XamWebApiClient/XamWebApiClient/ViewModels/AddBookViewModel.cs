using Android.Graphics;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamWebApiClient.Models;
using XamWebApiClient.Services;

namespace XamWebApiClient.ViewModels
{
    public class AddBookViewModel : BaseViewModel
    {
        private readonly IBookService _bookService;
        private string title;
        private string author;
        private string description;
       

        private byte[] _imageArray;

        public byte[] imageArray
        {
            get { return _imageArray; }
            set { _imageArray = value; }
        }



        public AddBookViewModel(IBookService bookService)
        {
            _bookService = bookService;

            SaveBookCommand = new Command(async () => await SaveBook());
            UploadBookImageCommand = new Command(async () => await UploadPhoto());
        }



        private async Task UploadPhoto()
         {
            try
            {
                await CrossMedia.Current.Initialize();

           
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality  =   40
            });
            imageArray = System.IO.File.ReadAllBytes(file.Path);
            return;
                /*Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        private async Task SaveBook()
        {
            try
            {
                var book = new AdddBook
                {
                    Title = Title,
                    Author = Author,
                    Description = Description,
                    ImageArray = imageArray

                };

                await _bookService.AddBook(book);              

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string Title
        {
            get => title; 
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Author
        {
            get => author; 
            set
            {
                author = value;
                OnPropertyChanged(nameof(Author));
            }
        }
        public string Description
        {
            get => description; 
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public ICommand SaveBookCommand { get; }
        public ICommand UploadBookImageCommand { get; }

    }
}
