package projects.myapiary

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.AdapterView
import android.widget.Button
import android.widget.ListView
import android.widget.Toast
import projects.myapiary.adapters.DeviceAdapter
import projects.myapiary.adapters.SessionAdapter
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.Device
import projects.myapiary.models.Session
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response


class ActivitySessionsScreen : AppCompatActivity() {

    private var sessionsList: ListView? = null
    private var buttonGoBackFromSessions: Button? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_sessions_screen)

        sessionsList = findViewById(R.id.sessionsList)
        buttonGoBackFromSessions = findViewById(R.id.buttonGoBackFromSessions)

        val context = this

        buttonGoBackFromSessions?.setOnClickListener {
            /*val intent = Intent (context, ActivityDevicesScreen::class.java)
            startActivity(intent)*/
            onBackPressed();
        }


        /// REQUEST FOR GET APIARIES


        val request = ResClientBuilder.buildClient(ResRequests::class.java)
        val response = request.getSessions(Storage.Selected_device_id, "Bearer ${Storage.token_short}")

        response.enqueue(object: Callback<List<Session>> {
            override fun onResponse(
                call: Call<List<Session>>,
                response: Response<List<Session>>
            ) {
                if (response.isSuccessful) {
                    val data = response.body()!!

                    var list = data.toList()

                    sessionsList?.adapter = SessionAdapter(context, R.layout.session_row, list)
                    sessionsList?.setOnItemClickListener { parent: AdapterView<*>, view: View, position: Int, id: Long ->
                        //Storage.Selected_device_id = list[position].Id
                        /*val intent = Intent (context, ActivityBeehivesScreen::class.java)
                        requireActivity().startActivity(intent)*/
                    }

                } else {
                    when(response.code()) {
                        404 -> {
                            Toast.makeText(context, "Сесій не знайдено", Toast.LENGTH_SHORT).show()
                        }
                        400 -> {
                            Toast.makeText(context, "Помилка при перевірці ИД 400", Toast.LENGTH_SHORT).show()
                        }
                        else -> {
                            Toast.makeText(context, "Сталася помилка при виведенні сесій1", Toast.LENGTH_SHORT).show()
                        }
                    }
                }
            }

            override fun onFailure(call: Call<List<Session>>, t: Throwable) {
                Toast.makeText(context, "Сталася помилка при виведенні сесій2", Toast.LENGTH_SHORT).show()
            }
        })

        /// REQUEST FOR GET APIARIES END

    }
}