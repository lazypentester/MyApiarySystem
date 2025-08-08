package projects.myapiary

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.ListView
import android.widget.Toast
import com.google.gson.JsonObject
import projects.myapiary.api.server.auth.AuthClientBuilder
import projects.myapiary.api.server.auth.AuthRequests
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.*
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Response
import javax.security.auth.callback.Callback
import kotlin.math.log

class ActivityProfileScreen : AppCompatActivity() {

    private var textViewProfEditName: EditText? = null
    private var textViewProfEditSurname: EditText? = null
    private var textViewProfEditMail: EditText? = null
    private var textViewProfEditPhone: EditText? = null
    private var textViewProfEditTariff: EditText? = null

    private var buttonSaveProfileSettings: Button? = null
    private var buttonCancelSavingProfSettings: Button? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_profile_screen)

        textViewProfEditName = findViewById(R.id.textViewProfEditName)
        textViewProfEditSurname = findViewById(R.id.textViewProfEditSurname)
        textViewProfEditMail = findViewById(R.id.textViewProfEditMail)
        textViewProfEditPhone = findViewById(R.id.textViewProfEditPhone)
        textViewProfEditTariff = findViewById(R.id.textViewProfEditTariff)

        buttonSaveProfileSettings = findViewById(R.id.buttonSaveProfileSettings)
        buttonCancelSavingProfSettings = findViewById(R.id.buttonCancelSavingProfSettings)

        val context = this

        buttonCancelSavingProfSettings?.setOnClickListener {
            onBackPressed();
        }


        /// REQUEST FOR GET USER


        val requests = ResClientBuilder.buildClient(ResRequests::class.java)
        val response = requests.getUserById(Storage.id, "Bearer ${Storage.token_short}")

        response.enqueue(object: retrofit2.Callback<UserSettings> {
            override fun onResponse(call: Call<UserSettings>, response: Response<UserSettings>) {
                if (response.isSuccessful) {

                    val data = response.body()!!

                    Storage.id = data.Id
                    Storage.name = data.Name
                    Storage.surname = data.Surname
                    Storage.phone = data.Phone
                    Storage.mail = data.Mail
                    Storage.tariff = data.Tariff


                    textViewProfEditName?.setText(Storage.name)
                    textViewProfEditSurname?.setText(Storage.surname)
                    textViewProfEditMail?.setText(Storage.mail)
                    textViewProfEditPhone?.setText(Storage.phone)
                    textViewProfEditTariff?.setText(Storage.tariff)

                } else {
                    Toast.makeText(context, "Помилка при запиті по користувачу", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<UserSettings>, t: Throwable) {
                Toast.makeText(context, "Помилка при запиті по користувачу", Toast.LENGTH_SHORT).show()
            }
        })

        /// REQUEST FOR GET USER END



        buttonSaveProfileSettings?.setOnClickListener {



            /// REQUEST FOR UPDATE USER


            val requestUpdateUsr = ResClientBuilder.buildClient(ResRequests::class.java)
            val responseUpdateUsr = requestUpdateUsr.editProfile("Bearer ${Storage.token_short}", UserSettings(
                Storage.id,
                textViewProfEditName?.text.toString(),
                textViewProfEditSurname?.text.toString(),
                textViewProfEditMail?.text.toString(),
                textViewProfEditPhone?.text.toString(),
                textViewProfEditTariff?.text.toString()
            ))

            responseUpdateUsr.enqueue(object: retrofit2.Callback<JsonObject> {
                override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                    if (response.isSuccessful) {

                        Toast.makeText(context, "Данні збережено", Toast.LENGTH_SHORT).show()
                        val data = response.body()!!

                        Storage.id

                        Storage.name = textViewProfEditName?.text.toString()
                        Storage.surname = textViewProfEditName?.text.toString()

                        onBackPressed()

                    } else {
                        Toast.makeText(context, "Помилка при оновленні користувача", Toast.LENGTH_SHORT).show()
                    }
                }

                override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                    Toast.makeText(context, "Помилка при оновленні користувача", Toast.LENGTH_SHORT).show()
                }
            })

            /// REQUEST FOR UPDATE USER END


        }
    }
}