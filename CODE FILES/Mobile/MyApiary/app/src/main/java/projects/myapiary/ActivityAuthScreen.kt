package projects.myapiary

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import org.json.JSONObject
import projects.myapiary.api.server.auth.AuthClientBuilder
import projects.myapiary.api.server.auth.AuthRequests
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.CreateSession
import projects.myapiary.models.LoginRequest
import projects.myapiary.models.LoginResponce
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ActivityAuthScreen : AppCompatActivity() {

    private var buttonAuth: Button? = null
    private var inputMail: EditText? = null
    private var inputPass: EditText? = null
    private  var btnGoRegister: TextView? = null

    val context = this

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_auth_screen)

        buttonAuth = findViewById(R.id.button_get_started_reg)
        inputMail = findViewById(R.id.inputEmailAddress)
        inputPass = findViewById(R.id.inputPassword)
        btnGoRegister = findViewById(R.id.btn_go_to_register)

        btnGoRegister?.setOnClickListener {
            val intent = Intent(context, ActivityRegisterScreen::class.java)
            startActivity(intent)
        }

        buttonAuth?.setOnClickListener {
            buttonAuth?.isEnabled = false
            if(inputMail?.text?.toString()?.equals("")!! || inputPass?.text?.toString()?.equals("")!!){
                Toast.makeText(context, "Одне з полів не заповнено.", Toast.LENGTH_SHORT).show()
            } else {

                var mail = inputMail?.text.toString()
                var pass = inputPass?.text.toString()

                Storage.mail = mail
                Storage.pass = pass

                val requests = AuthClientBuilder.buildClient(AuthRequests::class.java)
                val response = requests.login(LoginRequest(mail, pass))

                Toast.makeText(context, "Авторизація..", Toast.LENGTH_SHORT).show()

                response.enqueue(object: Callback<LoginResponce> {
                    override fun onResponse(call: Call<LoginResponce>, response: Response<LoginResponce>) {
                        if (response.isSuccessful) {

                            val data = response.body()!!

                            Storage.token_short = data.access_token_short
                            Storage.token_long = data.access_token_long
                            Storage.id = data.user_id
                            Storage.name = data.name
                            Storage.surname = data.surname
                            Storage.phone = data.phone
                            Storage.mail = data.mail
                            Storage.tariffId = data.tariffId
                            Storage.tariffName = data.tariff_name
                            Storage.max_apiaries = data.max_apiaries
                            Storage.max_beehives = data.max_beehives
                            Storage.tariff_price = data.price


                            // START CREATE SESSION AND FIND OR CREATE DEVICE

                            Toast.makeText(context, "Створення сесії..", Toast.LENGTH_SHORT).show()

                            val requestsCreateSession = ResClientBuilder.buildClient(ResRequests::class.java)
                            val responseCreateSession = requestsCreateSession.createSessionAndDevice(Storage.id, "Bearer ${Storage.token_short}", CreateSession(
                                0,
                                Storage.Device_Name,
                                Storage.Device_Mac_address,
                                Storage.Device_Fingerprint,
                                Storage.token_long,
                                0,
                                Storage.Session_Ip_address,
                                Storage.Session_Geolocation
                            ))

                            responseCreateSession.enqueue(object: Callback<CreateSession> {
                                override fun onResponse(call: Call<CreateSession>, response: Response<CreateSession>) {
                                    if (response.isSuccessful) {

                                        Toast.makeText(context, "Сесія успішно створена", Toast.LENGTH_SHORT).show()

                                        val data = response.body()!!

                                        Storage.Device_id = data.Device_id
                                        Storage.Session_Id = data.Session_Id

                                        buttonAuth?.isEnabled = true

                                        val intent = Intent(context, ActivityApiariesScreen::class.java)
                                        startActivity(intent)  // ТОЧКА ВХОДА В ПРИЛОЖЕНИЕ В АКТИВИТИ "ПАСЕКИ"

                                    } else {
                                        when(response.code()) {
                                            404 -> {
                                                Toast.makeText(context, "Помилка при створенні сесії 404", Toast.LENGTH_SHORT).show()
                                                buttonAuth?.isEnabled = true
                                            }
                                            400 -> {
                                                Toast.makeText(context, "Помилка при створенні сесії 400", Toast.LENGTH_SHORT).show()
                                                buttonAuth?.isEnabled = true
                                            }
                                            410 -> {
                                                Toast.makeText(context, "Помилка при створенні сесії 410", Toast.LENGTH_SHORT).show()
                                                buttonAuth?.isEnabled = true
                                            }
                                            else -> {
                                                Toast.makeText(context, "Сталася помилка при створенні сесії", Toast.LENGTH_SHORT).show()
                                                buttonAuth?.isEnabled = true
                                            }
                                        }
                                    }
                                }

                                override fun onFailure(call: Call<CreateSession>, t: Throwable) {
                                    Toast.makeText(context, "Сталася помилка при створенні сесії2", Toast.LENGTH_SHORT).show()
                                    buttonAuth?.isEnabled = true
                                }
                            })


                            // END CREATE SESSION AND FIND OR CREATE DEVICE


                        } else {
                            when(response.code()) {
                                404 -> {
                                    Toast.makeText(context, "Користувача з таким логіном або паролем не існує", Toast.LENGTH_SHORT).show()
                                    buttonAuth?.isEnabled = true
                                }
                                400 -> {
                                    Toast.makeText(context, "Невірний формат пошти", Toast.LENGTH_SHORT).show()
                                    buttonAuth?.isEnabled = true
                                }
                                410 -> {
                                    Toast.makeText(context, "Треба підтвердити аккаунт", Toast.LENGTH_SHORT).show()
                                    val intent = Intent(context, ActivityConfirmCodeChoiseScreen::class.java)
                                    startActivity(intent)
                                }
                                else -> {
                                    Toast.makeText(context, "Сталася помилка при авторизації", Toast.LENGTH_SHORT).show()
                                    buttonAuth?.isEnabled = true
                                }
                            }
                        }
                    }

                    override fun onFailure(call: Call<LoginResponce>, t: Throwable) {
                        Toast.makeText(context, "Сталася помилка(", Toast.LENGTH_SHORT).show()
                        buttonAuth?.isEnabled = true
                    }
                })
            }
        }
    }
}