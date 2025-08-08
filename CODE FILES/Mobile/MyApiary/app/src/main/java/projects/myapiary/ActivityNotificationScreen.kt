package projects.myapiary

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.AdapterView
import android.widget.Button
import android.widget.ListView
import android.widget.Toast
import com.google.gson.JsonObject
import projects.myapiary.adapters.NotificationAdapter
import projects.myapiary.adapters.SessionAdapter
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.NotificationsByApiary
import projects.myapiary.models.Session
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ActivityNotificationScreen : AppCompatActivity() {

    private var notificationsList: ListView? = null
    private var buttonGoBackFromNotifications: Button? = null

    private var name_note: String? = null
    private var created_date_note: String? = null
    private var description_note: String? = null

    val context = this

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_notification_screen)


        notificationsList = findViewById(R.id.notificationsList)
        buttonGoBackFromNotifications = findViewById(R.id.buttonGoBackFromNotifications)

        buttonGoBackFromNotifications?.setOnClickListener {
            onBackPressed();
        }

        getNotifications()

    }

    override fun onRestart() {
        super.onRestart()
        getNotifications()
    }

    private fun getNotifications() {
        /// REQUEST FOR GET NOTIFICATIONS


        val request = ResClientBuilder.buildClient(ResRequests::class.java)
        val response = request.GetNotificationByApiary(intent.extras!!.getInt("apiary_id"), "Bearer ${Storage.token_short}")

        response.enqueue(object: Callback<List<NotificationsByApiary>> {
            override fun onResponse(
                call: Call<List<NotificationsByApiary>>,
                response: Response<List<NotificationsByApiary>>
            ) {
                if (response.isSuccessful) {
                    val data = response.body()!!

                    var list = data.toList()

                    notificationsList?.adapter = NotificationAdapter(context, R.layout.notification_row, list)

                    notificationsList?.setOnItemClickListener { parent: AdapterView<*>, view: View, position: Int, id: Long ->
                        Storage.Selected_notification_id = list[position].Id
                        name_note = list[position].Name
                        created_date_note = list[position].Created_date
                        description_note = list[position].Description


                        /// REQUEST TO READ NOTIF.


                        val requestReadNode = ResClientBuilder.buildClient(ResRequests::class.java)
                        val responseReadNode = requestReadNode.readNotification(Storage.Selected_notification_id, "Bearer ${Storage.token_short}")

                        responseReadNode.enqueue(object: Callback<JsonObject> {
                            override fun onResponse(
                                call: Call<JsonObject>,
                                response: Response<JsonObject>
                            ) {
                                if (response.isSuccessful) {
                                    val dataReadNote = response.body()!!

                                    val intent = Intent (context, ActivityNotificationSinglePage::class.java)
                                    intent.putExtra("name", name_note)
                                    intent.putExtra("date", created_date_note)
                                    intent.putExtra("desc", description_note)
                                    context.startActivity(intent)

                                } else {
                                    when(response.code()) {
                                        404 -> {
                                            Toast.makeText(context, "Сторінку не знайдено", Toast.LENGTH_SHORT).show()
                                        }
                                        400 -> {
                                            Toast.makeText(context, "Помилка при перевірці ИД 400", Toast.LENGTH_SHORT).show()
                                        }
                                        else -> {
                                            Toast.makeText(context, "Сталася помилка при запиті на читання повідомлення1", Toast.LENGTH_SHORT).show()
                                        }
                                    }
                                }
                            }

                            override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                                Toast.makeText(context, "Сталася помилка при запиті на читання повідомлення2", Toast.LENGTH_SHORT).show()
                            }
                        })


                        /// REQUEST TO READ NOTIF. END

                    }

                } else {
                    when(response.code()) {
                        404 -> {
                            Toast.makeText(context, "Повідомлень не знайдено", Toast.LENGTH_SHORT).show()
                        }
                        400 -> {
                            Toast.makeText(context, "Помилка при перевірці ИД 400", Toast.LENGTH_SHORT).show()
                        }
                        else -> {
                            Toast.makeText(context, "Сталася помилка при виведенні повідомлень", Toast.LENGTH_SHORT).show()
                        }
                    }
                }
            }

            override fun onFailure(call: Call<List<NotificationsByApiary>>, t: Throwable) {
                Toast.makeText(context, "Сталася помилка при виведенні повідомлень2", Toast.LENGTH_SHORT).show()
            }
        })

        /// REQUEST FOR GET NOTIFICATIONS END
    }
}