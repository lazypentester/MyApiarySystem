package projects.myapiary

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.google.gson.JsonObject
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.ApiaryUpdate
import projects.myapiary.models.BeehiveUpdate
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ActivityUpdateBeehiveScreen : AppCompatActivity() {

    private var buttonCancelUpdateBee: Button? = null
    private var buttonUpdateBeehive: Button? = null

    private var beehiveUpdateName: EditText? = null

    val context = this

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_update_beehive_screen)

        buttonCancelUpdateBee = findViewById(R.id.buttonCancelUpdateBee)
        buttonUpdateBeehive = findViewById(R.id.buttonUpdateBeehive)

        beehiveUpdateName = findViewById(R.id.beehiveUpdateName)

        buttonCancelUpdateBee?.setOnClickListener {
            onBackPressed()
        }

        beehiveUpdateName?.setText(intent.extras!!.get("name").toString())

        buttonUpdateBeehive?.setOnClickListener {

            buttonUpdateBeehive?.isEnabled = false
            buttonCancelUpdateBee?.isEnabled = false

            //UPDATE BEE START

            val nameBeehive = beehiveUpdateName?.text.toString()

            if(nameBeehive == "") {
                Toast.makeText(context, "Поле 'Назва' не заповнено", Toast.LENGTH_SHORT).show()
                buttonUpdateBeehive?.isEnabled = true
                buttonCancelUpdateBee?.isEnabled = true
                return@setOnClickListener
            }

            val requestsUpdateBeehive = ResClientBuilder.buildClient(ResRequests::class.java)
            val responseUpdateBeehive = requestsUpdateBeehive.editBeehive(intent.extras!!.getInt("id"),"Bearer ${Storage.token_short}", BeehiveUpdate(
                intent.extras!!.getInt("id"),
                nameBeehive,
                false,
                intent.extras!!.getInt("ApiariId")
            ))

            responseUpdateBeehive.enqueue(object: Callback<JsonObject> {
                override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                    if (response.isSuccessful) {

                        Toast.makeText(context, "Данні вулика оновлені", Toast.LENGTH_SHORT).show()
                        onBackPressed()

                    } else {
                        when(response.code()) {
                            404 -> {
                                Toast.makeText(context, "Сторінку не знайдено 404", Toast.LENGTH_SHORT).show()
                                onBackPressed()
                            }
                            else -> {
                                Toast.makeText(context, "Сталася помилка при редагуванні данних вулика1", Toast.LENGTH_SHORT).show()
                                onBackPressed()
                            }
                        }
                    }
                }

                override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                    Toast.makeText(context, "Сталася помилка при редагуванні данних вулика2", Toast.LENGTH_SHORT).show()
                    onBackPressed()
                }
            })


            // ADD APIARY END

        }

    }
}