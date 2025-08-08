package projects.myapiary

import android.content.Intent
import android.nfc.Tag
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.CountDownTimer
import android.util.Log
import android.view.ContextMenu
import android.view.MenuItem
import android.view.View
import android.widget.AdapterView
import android.widget.Button
import android.widget.ListView
import android.widget.Toast
import com.google.gson.JsonObject
import projects.myapiary.adapters.ApiaryAdapter
import projects.myapiary.adapters.BeehiveAdapter
import projects.myapiary.api.server.ip.ipregistry.IpregistryClientBuilder
import projects.myapiary.api.server.ip.ipregistry.IpregistryRequests
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.Apiary
import projects.myapiary.models.Beehive
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.lang.Exception


class ActivityBeehivesScreen : AppCompatActivity() {

    private var beehivesList: ListView? = null
    private var buttonCreateNewBeehive: Button? = null
    private var timer: CountDownTimer? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_beehives_screen)

        buttonCreateNewBeehive = findViewById(R.id.buttonCreateNewBeehive)

        beehivesList = findViewById(R.id.beehivesList)

        val context = this

        buttonCreateNewBeehive?.setOnClickListener {

            stopUpdateBeehives()
            val intent = Intent (context, ActivityNewbeehiveScreen::class.java)
            startActivity(intent)
        }

        // context menu register
        registerForContextMenu(beehivesList)

        getBeehives()

        startUpdateBeehives()

    }

    /// START SET SETTINGS FOR CONTEXT MENU
    override fun onCreateContextMenu(menu: ContextMenu?, v: View?, menuInfo: ContextMenu.ContextMenuInfo?) {
        super.onCreateContextMenu(menu, v, menuInfo)
        val inflater = menuInflater
        inflater.inflate(R.menu.context_menu_beehives, menu)
    }

    override fun onContextItemSelected(item: MenuItem): Boolean {
        beehivesList = findViewById(R.id.beehivesList)
        val info = item.menuInfo as AdapterView.AdapterContextMenuInfo
        var position_selected_item = info.position
        var beehive = beehivesList?.getItemAtPosition(position_selected_item) as Beehive
        when(item.itemId) {
            R.id.edit_beehive ->{
                stopUpdateBeehives()
                val intent = Intent (this, ActivityUpdateBeehiveScreen::class.java)
                intent.putExtra("id", beehive.Id)
                intent.putExtra("name", beehive.Name)
                intent.putExtra("ApiariId", Storage.id)
                this.startActivity(intent)
                return true
            }
            R.id.delete_beehive ->{
                stopUpdateBeehives()
                deleteBeehive(beehive.Id)
                return true
            }
        }
        return super.onContextItemSelected(item)
    }
    /// END SET SETTINGS FOR CONTEXT MENU

    override fun onResume() {
        super.onResume()
        getBeehives()
        startUpdateBeehives()
    }

    private fun getBeehives() {

        val context = this

        /// REQUEST FOR GET BEEHIVES


        val request = ResClientBuilder.buildClient(ResRequests::class.java)
        val response = request.getBeehivesByApiaryId(Storage.Selected_apiary_id, "Bearer ${Storage.token_short}")

        response.enqueue(object: Callback<List<Beehive>> {
            override fun onResponse(
                call: Call<List<Beehive>>,
                response: Response<List<Beehive>>
            ) {
                if (response.isSuccessful) {
                    val data = response.body()!!

                    //var list = data.toList()
                    var list = data.toList()


                    beehivesList?.adapter = BeehiveAdapter(context, R.layout.beehive_row, list)

                    beehivesList?.setOnItemClickListener { parent: AdapterView<*>, view: View, position: Int, id: Long ->
                        Storage.Selected_beehive_id = list[position].Id

                        stopUpdateBeehives()

                        /// GET REQUEST FOR BEEHIVE SENSORS START

                        val requestSensors = ResClientBuilder.buildClient(ResRequests::class.java)
                        val responseSensors = requestSensors.getAllSensorsByBeehive(Storage.Selected_beehive_id, "Bearer ${Storage.token_short}")

                        responseSensors.enqueue(object: Callback<JsonObject> {
                            override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                                if (response.isSuccessful) {
                                    val data = response.body()!!

                                    val result = data.get("sensors_count").asInt

                                    if(result != 0) {
                                        val intent = Intent (context, ActivitySingleBeehiveScreen::class.java)
                                        context.startActivity(intent)
                                    } else {
                                        Toast.makeText(context, "У обраного вулика немає сенсорів", Toast.LENGTH_SHORT).show()
                                        startUpdateBeehives()
                                    }
                                } else {
                                    when(response.code()) {
                                        404 -> {
                                            Toast.makeText(context, "Сенсорів не знайдено", Toast.LENGTH_SHORT).show()
                                            startUpdateBeehives()
                                        }
                                        400 -> {
                                            Toast.makeText(context, "Помилка при підрахунку сенсорів вулика 400", Toast.LENGTH_SHORT).show()
                                            startUpdateBeehives()
                                        }
                                        else -> {
                                            Toast.makeText(context, "Сталася помилка при підрахунку сенсорів вулика", Toast.LENGTH_SHORT).show()
                                            startUpdateBeehives()
                                        }
                                    }
                                }
                            }

                            override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                                Toast.makeText(context, "Сталася помилка при підрахунку сенсорів вулика2", Toast.LENGTH_SHORT).show()
                                startUpdateBeehives()
                            }
                        })


                        /// GET REQUEST FOR BEEHIVE SENSORS END


                    }

                } else {
                    when(response.code()) {
                        404 -> {
                            Toast.makeText(context, "Вуликів не знайдено", Toast.LENGTH_SHORT).show()
                            startUpdateBeehives()
                        }
                        400 -> {
                            Toast.makeText(context, "Помилка при підрахунку праметрів вуликів 400", Toast.LENGTH_SHORT).show()
                            startUpdateBeehives()
                        }
                        else -> {
                            Toast.makeText(context, "Сталася помилка при виведенні вуликів1", Toast.LENGTH_SHORT).show()
                            startUpdateBeehives()
                        }
                    }
                }
            }

            override fun onFailure(call: Call<List<Beehive>>, t: Throwable) {
                Toast.makeText(context, "Сталася помилка при виведенні вуликів2", Toast.LENGTH_SHORT).show()
                startUpdateBeehives()
            }
        })

        /// REQUEST FOR GET BEEHIVES END
    }

    private fun deleteBeehive(apiary_id: Int) {

        stopUpdateBeehives()

        val context = this

        val request = ResClientBuilder.buildClient(ResRequests::class.java)
        val response = request.deleteBeehives(apiary_id, "Bearer ${Storage.token_short}")

        response.enqueue(object: Callback<JsonObject> {
            override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                if (response.isSuccessful) {
                    val data = response.body()!!
                    Toast.makeText(context, "Вулик видалений", Toast.LENGTH_SHORT).show()
                    getBeehives()
                } else {
                    when(response.code()) {
                        415 -> {
                            Toast.makeText(context, "Операція неможлива: за вуликом вже закріплені датчики", Toast.LENGTH_SHORT).show()
                            startUpdateBeehives()
                        }
                        404 -> {
                            Toast.makeText(context, "Вуликів не знайдено за заданим ИД", Toast.LENGTH_SHORT).show()
                            startUpdateBeehives()
                        }
                        400 -> {
                            Toast.makeText(context, "У цього користувача не існує такого вулика", Toast.LENGTH_SHORT).show()
                            startUpdateBeehives()
                        }
                        else -> {
                            Toast.makeText(context, "Сталася помилка при видаленні вулика", Toast.LENGTH_SHORT).show()
                            startUpdateBeehives()
                        }
                    }
                }
            }

            override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                Toast.makeText(context, "Помилка при видаленні вулика", Toast.LENGTH_SHORT).show()
                startUpdateBeehives()
            }
        })
    }

    private fun startUpdateBeehives(){
        startCountDownTimer()
    }

    private fun stopUpdateBeehives(){
        timer?.cancel()
    }

    private fun startCountDownTimer(){
        timer?.cancel()
        timer = object: CountDownTimer(10000, 1000) {
            override fun onTick(millisUntilFinished: Long) {
                //Toast.makeText(applicationContext, "+++", Toast.LENGTH_SHORT).show()
            }

            override fun onFinish() {
                try {
                    getBeehives()
                } catch (e: Exception){
                }
                startUpdateBeehives()
            }
        }.start()
    }
}