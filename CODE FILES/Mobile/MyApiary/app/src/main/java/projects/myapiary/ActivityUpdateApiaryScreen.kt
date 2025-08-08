package projects.myapiary

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.google.gson.JsonObject
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.ApiaryAdd
import projects.myapiary.models.ApiaryUpdate
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ActivityUpdateApiaryScreen : AppCompatActivity() {

    private var buttonCancelIUpdateAp: Button? = null
    private var buttonUpdateApiary: Button? = null

    private var apiaryUpdateName: EditText? = null
    private var apiary_address_update: EditText? = null

    val context = this



    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_update_apiary_screen)

        buttonCancelIUpdateAp = findViewById(R.id.buttonCancelIUpdateAp)
        buttonUpdateApiary = findViewById(R.id.buttonUpdateApiary)

        apiaryUpdateName = findViewById(R.id.apiaryUpdateName)
        apiary_address_update = findViewById(R.id.apiary_address_update)

        buttonCancelIUpdateAp?.setOnClickListener {
            onBackPressed()
        }

        apiaryUpdateName?.setText(intent.extras!!.get("name").toString())
        apiary_address_update?.setText(intent.extras!!.get("address").toString())

        buttonUpdateApiary?.setOnClickListener {

            buttonUpdateApiary?.isEnabled = false
            buttonCancelIUpdateAp?.isEnabled = false

            //ADD APIARY START

            val nameApiary = apiaryUpdateName?.text.toString()
            val addressApiary = apiary_address_update?.text.toString()

            if(nameApiary == "") {
                Toast.makeText(context, "Поле 'Назва' не заповнено", Toast.LENGTH_SHORT).show()
                buttonUpdateApiary?.isEnabled = true
                buttonCancelIUpdateAp?.isEnabled = true
                return@setOnClickListener
            }
            else if(addressApiary == "")
            {
                Toast.makeText(context, "Поле 'Адреса' не заповнено", Toast.LENGTH_SHORT).show()
                buttonUpdateApiary?.isEnabled = true
                buttonCancelIUpdateAp?.isEnabled = true
                return@setOnClickListener
            }

            val requestsUpdateApiary = ResClientBuilder.buildClient(ResRequests::class.java)
            val responseUpdateApiary = requestsUpdateApiary.editApiary(intent.extras!!.getInt("id"),"Bearer ${Storage.token_short}", ApiaryUpdate(
                intent.extras!!.getInt("id"),
                nameApiary,
                addressApiary,
                intent.extras!!.getInt("userid")
            ))

            responseUpdateApiary.enqueue(object: Callback<JsonObject> {
                override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                    if (response.isSuccessful) {

                        Toast.makeText(context, "Данні пасіки оновлені", Toast.LENGTH_SHORT).show()
                        /*val intent = Intent(context, ActivityApiariesScreen::class.java)
                        startActivity(intent)*/
                        onBackPressed()

                    } else {
                        when(response.code()) {
                            404 -> {
                                Toast.makeText(context, "Сторінку не знайдено 404", Toast.LENGTH_SHORT).show()
                                /*val intent = Intent(context, ActivityApiariesScreen::class.java)
                                startActivity(intent)*/
                                onBackPressed()
                            }
                            else -> {
                                Toast.makeText(context, "Сталася помилка при редагуванні данних пасіки1", Toast.LENGTH_SHORT).show()
                                /*val intent = Intent(context, ActivityApiariesScreen::class.java)
                                startActivity(intent)*/
                                onBackPressed()
                            }
                        }
                    }
                }

                override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                    Toast.makeText(context, "Сталася помилка при редагуванні данних пасіки1", Toast.LENGTH_SHORT).show()
                    /*val intent = Intent(context, ActivityApiariesScreen::class.java)
                    startActivity(intent)*/
                    onBackPressed()
                }
            })


            // ADD APIARY END

        }


    }
}