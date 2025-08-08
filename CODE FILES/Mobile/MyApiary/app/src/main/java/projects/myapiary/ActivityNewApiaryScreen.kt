package projects.myapiary

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.google.gson.JsonObject
import projects.myapiary.api.server.auth.AuthClientBuilder
import projects.myapiary.api.server.auth.AuthRequests
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.ApiaryAdd
import projects.myapiary.models.LoginRequest
import projects.myapiary.models.LoginResponce
import projects.myapiary.storage.Storage
import projects.myapiary.storage.Storage.Companion.AddApiarySelected
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ActivityNewApiaryScreen : AppCompatActivity() {

    private var buttonCancelCreateAp: Button? = null
    private var buttonCreateApiary: Button? = null

    private var apiaryNewName: EditText? = null
    private var apiary_address_new: EditText? = null

    val context = this

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_new_apiary_screen)

        buttonCancelCreateAp = findViewById(R.id.buttonCancelCreateAp)
        buttonCreateApiary = findViewById(R.id.buttonCreateApiary)

        apiaryNewName = findViewById(R.id.apiaryNewName)
        apiary_address_new = findViewById(R.id.apiary_address_new)

        buttonCancelCreateAp?.setOnClickListener {
            onBackPressed()
        }

        buttonCreateApiary?.setOnClickListener {

            buttonCreateApiary?.isEnabled = false
            buttonCancelCreateAp?.isEnabled = false

            //ADD APIARY START

            var nameApiary = apiaryNewName?.text.toString()
            var addressApiary = apiary_address_new?.text.toString()

            if(nameApiary == "") {
                Toast.makeText(context, "Поле 'Назва' не заповнено", Toast.LENGTH_SHORT).show()
                buttonCreateApiary?.isEnabled = true
                buttonCancelCreateAp?.isEnabled = true
                return@setOnClickListener
            }
            else if(addressApiary == "")
            {
                Toast.makeText(context, "Поле 'Адреса' не заповнено", Toast.LENGTH_SHORT).show()
                buttonCreateApiary?.isEnabled = true
                buttonCancelCreateAp?.isEnabled = true
                return@setOnClickListener
            }

            val requestsAddApiary = ResClientBuilder.buildClient(ResRequests::class.java)
            val responseAddApiary = requestsAddApiary.addNewApiary("Bearer ${Storage.token_short}", ApiaryAdd(nameApiary, addressApiary, Storage.id))

            responseAddApiary.enqueue(object: Callback<JsonObject> {
                override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                    if (response.isSuccessful) {

                        Toast.makeText(context, "Нову пасіку успішно створено", Toast.LENGTH_SHORT).show()
                        onBackPressed()

                    } else {
                        when(response.code()) {
                            404 -> {
                                Toast.makeText(context, "Сторінку не знайдено 404", Toast.LENGTH_SHORT).show()
                                onBackPressed()
                            }
                            400 -> {
                                Toast.makeText(context, "Досягнута максимальна кількість пасік для вашого тарифу.\nАбо не співпадає ИД.", Toast.LENGTH_SHORT).show()
                                onBackPressed()
                            }
                            else -> {
                                Toast.makeText(context, "Сталася помилка при додаванні нової пасіки1", Toast.LENGTH_SHORT).show()
                                onBackPressed()
                            }
                        }
                    }
                }

                override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                    Toast.makeText(context, "Сталася помилка при додаванні нової пасіки2", Toast.LENGTH_SHORT).show()
                    onBackPressed()
                }
            })


            // ADD APIARY END

        }

    }
}