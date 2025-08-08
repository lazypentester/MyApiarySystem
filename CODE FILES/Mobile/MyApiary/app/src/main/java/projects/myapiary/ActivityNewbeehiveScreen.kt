package projects.myapiary

import android.app.PendingIntent.getActivity
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
import projects.myapiary.models.BeehiveAdd
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ActivityNewbeehiveScreen : AppCompatActivity() {

    private var buttonCreateBeehive: Button? = null
    private var buttonCancelCreateBee: Button? = null

    private var beehiveNewName: EditText? = null

    val context = this

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_newbeehive_screen)

        buttonCreateBeehive = findViewById(R.id.buttonCreateBeehive)
        buttonCancelCreateBee = findViewById(R.id.buttonCancelCreateBee)

        beehiveNewName = findViewById(R.id.beehiveNewName)

        buttonCancelCreateBee?.setOnClickListener {
            /*val intent = Intent (context, ActivityBeehivesScreen::class.java)
            startActivity(intent)*/
            onBackPressed()
        }


        buttonCreateBeehive?.setOnClickListener {

            buttonCreateBeehive?.isEnabled = false
            buttonCancelCreateBee?.isEnabled = false

                //ADD BEEHIVE START

                var nameBeehive = beehiveNewName?.text.toString()

                if(nameBeehive == "") {
                    Toast.makeText(context, "Поле 'Назва' не заповнено", Toast.LENGTH_SHORT).show()
                    buttonCreateBeehive?.isEnabled = true
                    buttonCancelCreateBee?.isEnabled = true
                    return@setOnClickListener
                }

                val requestsAddBeehive = ResClientBuilder.buildClient(ResRequests::class.java)
                val responseAddBeehive = requestsAddBeehive.addNewBeehive("Bearer ${Storage.token_short}", BeehiveAdd(nameBeehive, false, Storage.Selected_apiary_id))

            responseAddBeehive.enqueue(object: Callback<JsonObject> {
                    override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                        if (response.isSuccessful) {

                            Toast.makeText(context, "Новий вулик успішно створено", Toast.LENGTH_SHORT).show()
                            val intent = Intent(context, ActivityBeehivesScreen::class.java)
                            startActivity(intent)

                        } else {
                            when(response.code()) {
                                404 -> {
                                    Toast.makeText(context, "Сторінку не знайдено 404", Toast.LENGTH_SHORT).show()
                                    val intent = Intent(context, ActivityBeehivesScreen::class.java)
                                    startActivity(intent)
                                }
                                400 -> {
                                    Toast.makeText(context, "Досягнута максимальна кількість вуликів для вашого тарифу.\nАбо не співпадає ИД.", Toast.LENGTH_SHORT).show()
                                    val intent = Intent(context, ActivityBeehivesScreen::class.java)
                                    startActivity(intent)
                                }
                                else -> {
                                    Toast.makeText(context, "Сталася помилка при додаванні нового вулика1", Toast.LENGTH_SHORT).show()
                                    val intent = Intent(context, ActivityBeehivesScreen::class.java)
                                    startActivity(intent)
                                }
                            }
                        }
                    }

                    override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                        Toast.makeText(context, "Сталася помилка при додаванні нового вулика1", Toast.LENGTH_SHORT).show()
                        val intent = Intent(context, ActivityBeehivesScreen::class.java)
                        startActivity(intent)
                    }
                })


                // ADD APIARY END

            }


    }
}