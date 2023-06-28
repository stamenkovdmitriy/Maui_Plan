using Plugin.Maui.Audio;
using System.Timers;
using System.Threading.Tasks;


namespace Maui_Plan;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
        

        
    }
    

    private void LoadData()
    {
        // Загрузка сохраненных данных
        text_Tema.Text = Preferences.Get("text_Tema", "");
        text_celiZadachi.Text = Preferences.Get("text_celiZadachi", "");
        text_oborudovanie.Text = Preferences.Get("text_oborudovanie", "");
        text_VvodnayaChast.Text = Preferences.Get("text_VvodnayaChast", "");
        text_OsnovnayaChast.Text = Preferences.Get("text_OsnovnayaChast", "");
        text_ZacluchitelnayaChast.Text = Preferences.Get("text_ZacluchitelnayaChast", "");
    }

    private void SaveData()
    {
        // Сохранение данных
        Preferences.Set("text_Tema", text_Tema.Text);
        Preferences.Set("text_celiZadachi", text_celiZadachi.Text);
        Preferences.Set("text_oborudovanie", text_oborudovanie.Text);
        Preferences.Set("text_VvodnayaChast", text_VvodnayaChast.Text);
        Preferences.Set("text_OsnovnayaChast", text_OsnovnayaChast.Text);
        Preferences.Set("text_ZacluchitelnayaChast", text_ZacluchitelnayaChast.Text);
    }
    private void save_Clicked(object sender, EventArgs e)
    {
        SaveData();
    }
    private void open_Clicked(object sender, EventArgs e)
    {
        LoadData();
    }

    private bool isTimerRunning = false;
    private int timerDuration = 0;
    [Obsolete]
    private void startZanyatie_Clicked(object sender, EventArgs e)
    {
        
        int seconds = 0;
        // Если удалось успешно преобразовать значение в целое число,
        // то мы создаем таймер с интервалом в 1 секунду, используя метод 
        // "Device.StartTimer"
        if (int.TryParse(time_VvodnayaChast.Text, out seconds))
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
        // уменьшаем значение "seconds" на 1, изменяя значения свойств "Text"
        // элемента "timer_VvodnayaChast" и "Value" элемента "progres_VvodnayaChast"
                seconds--;
                timer_VvodnayaChast.Text = seconds.ToString();
                progres_VvodnayaChast.Value = seconds;
        // Если значение "seconds" становится меньше или равно 0, то мы выполняем 
        // необходимое действие по завершению таймера и возвращаем "false" для 
        // остановки таймера. Если же значение "seconds" больше 0, то мы
        // возвращаем "true" для продолжения работы таймера
                if (seconds <= 0)
                {

                    int seconds2 = 0;
                    if (int.TryParse(time_OsnvnayaChast.Text, out seconds2))
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                        {
                            seconds2--;
                            timer_OsnvnayaChast.Text = seconds2.ToString();
                            progres_OsnvnayaChast.Value = seconds2;
                            if (seconds2 <= 0)
                            {

                                int seconds3 = 0;
                                if (int.TryParse(time_ZacluchitelnayaChast.Text, out seconds3))
                                {
                                    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                                    {
                                        seconds3--;
                                        timer_ZacluchitelnayaChast.Text = seconds3.ToString();
                                        progres_ZacluchitelnayaChast.Value = seconds3;
                                        if (seconds3 <= 0)
                                        {
                                            // Действия по завершению таймера
                                            PlayAudio();
                                            return false; // Остановить таймер
                                        }
                                        return true; // Продолжить таймер
                                    });
                                }
                                else
                                {
                                    // Обработка ошибки ввода времени
                                    DisplayAlert("Ошибка", "Неверное значение таймера", "OK");
                                }

                                // Действия по завершению таймера
                                PlayAudio();
                                return false; // Остановить таймер
                            }
                            return true; // Продолжить таймер
                        });
                    }
                    else
                    {
                        // Обработка ошибки ввода времени
                        DisplayAlert("Ошибка", "Неверное значение таймера", "OK");
                    }

                    // Действия по завершению таймера

                    PlayAudio();

                    return false; // Остановить таймер
                                        
                }

                return true; // Продолжить таймер
            });
        }
        else
        {
            // Обработка ошибки ввода времени
            DisplayAlert("Ошибка", "Неверное значение таймера", "OK");
        }

        
    }

    public async void PlayAudio()
     {
            var audioPlayer = AudioManager.Current.CreatePlayer
                (await FileSystem.OpenAppPackageFileAsync("Resources/Audio/solovey.mp3"));

            audioPlayer.Play();
     }

    
}

