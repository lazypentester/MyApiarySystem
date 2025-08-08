package projects.myapiary

import android.annotation.SuppressLint
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.Settings
import android.widget.Button
import projects.myapiary.Helpers.GetSystemInfo
import projects.myapiary.Helpers.PublicIp
import projects.myapiary.storage.Storage
class ActivityWelcomeScreen : AppCompatActivity() {

    private var buttonGetStartedReg: Button? = null;


    @SuppressLint("HardwareIds")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_welcome_screen)


        buttonGetStartedReg = findViewById(R.id.button_get_started_reg);
        buttonGetStartedReg?.setOnClickListener {
            val intent = Intent(this, ActivityRegisterScreen::class.java)
            startActivity(intent)
        }


        // ПЕРЕНЕСТИ ЗАПРОСЫ НА ПОЛУЧЕНИЕ ДАННЫХ НА СЕРВЕР, ЧТОБЫ СЕРВЕР ПОЛУЧАЛ ПОДРОБНОСТИ О КЛИЕНТЕ И СРАЗУ ЗАПИСЫВАЛ В БД И ВЫДАВАЛ КЛИЕНТУ
        // КАК? - НА КЛИЕНТЕ ДЕЛАЕМ ЗАПРОС И ПОЛУЧАЕМ ПАБЛИК ИП, ПОСЛЕ ОТСЫЛАЕМ ЕГО НА СЕРВЕР И ОН УЖЕ ДЕЛАЕТ ВСЮ ОСТАЛЬНУЮ РАБОТУ(ШЛЕ ЗАПРОСЫ НА СТОРОННИЕ РЕСУРСЫ)
        //  А ПОКА ЧТО ДЕЛАТЬ ЭТО ВСЕ НА КЛИЕНТЕ
        // А так же брать всю эту инфу именно во время авторизации
        Storage.Device_Name = GetSystemInfo.parseStringLocale().toString()
        //Storage.Device_Name = Settings.System.NAME
        //Storage.Device_Name = Settings.Global.DEVICE_NAME
        //Storage.Device_Name = Settings.Global.NAME
        Storage.Device_Fingerprint = Settings.Secure.getString(contentResolver, Settings.Secure.ANDROID_ID)
        ////// ДОДЛЕАТЬ ГЕТАТЬ ВНЕШНИЙ ИП АДРЕСС + ГЕОЛОКАЦИЮ ПО ГЕТ ЗАПРОСУ
        PublicIp.GetFromIpify().toString()
        PublicIp.GetInfoAboutClientIp()

    }
}