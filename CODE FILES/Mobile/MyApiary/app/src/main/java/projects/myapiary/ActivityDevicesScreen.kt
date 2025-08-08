package projects.myapiary

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.AdapterView
import android.widget.Button
import android.widget.ListView
import android.widget.Toast
import projects.myapiary.adapters.BeehiveAdapter
import projects.myapiary.adapters.DeviceAdapter
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.Beehive
import projects.myapiary.models.Device
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ActivityDevicesScreen : AppCompatActivity() {

    private var devicesList: ListView? = null
    private var buttonGoBack: Button? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_devices_screen)

        devicesList = findViewById(R.id.devicesList)
        buttonGoBack = findViewById(R.id.buttonGoBack)

        val context = this

        buttonGoBack?.setOnClickListener {
            /*val intent = Intent (context, ActivityApiariesScreen::class.java)
            startActivity(intent)*/
            onBackPressed();
        }

        /// REQUEST FOR GET APIARIES


        val request = ResClientBuilder.buildClient(ResRequests::class.java)
        val response = request.getDevices(Storage.id, "Bearer ${Storage.token_short}")

        response.enqueue(object: Callback<List<Device>> {
            override fun onResponse(
                call: Call<List<Device>>,
                response: Response<List<Device>>
            ) {
                if (response.isSuccessful) {
                    val data = response.body()!!

                    var list = data.toList()

                    devicesList?.adapter = DeviceAdapter(context, R.layout.device_row, list)
                    devicesList?.setOnItemClickListener { parent: AdapterView<*>, view: View, position: Int, id: Long ->
                        Storage.Selected_device_id = list[position].Id
                        val intent = Intent (context, ActivitySessionsScreen::class.java)
                        startActivity(intent)
                    }

                } else {
                    when(response.code()) {
                        404 -> {
                            Toast.makeText(context, "Девайсів не знайдено", Toast.LENGTH_SHORT).show()
                        }
                        400 -> {
                            Toast.makeText(context, "Помилка при перевірці ИД 400", Toast.LENGTH_SHORT).show()
                        }
                        else -> {
                            Toast.makeText(context, "Сталася помилка при виведенні девайсів1", Toast.LENGTH_SHORT).show()
                        }
                    }
                }
            }

            override fun onFailure(call: Call<List<Device>>, t: Throwable) {
                Toast.makeText(context, "Сталася помилка при виведенні девайсів2", Toast.LENGTH_SHORT).show()
            }
        })

        /// REQUEST FOR GET APIARIES END

    }
}