package projects.myapiary

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.*
import com.google.gson.JsonObject
import projects.myapiary.api.server.auth.AuthClientBuilder
import projects.myapiary.api.server.auth.AuthRequests
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.*
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ActivityConfirmCodeChoiseScreen : AppCompatActivity() {

    private var buttonSendCode: Button? = null
    private var buttonCheckCode: Button? = null
    private var inputCode: EditText? = null
    private  var radioButtonsChoise: RadioGroup? = null

    private var emailConfirm: Boolean = true
    private var phoneConfirm: Boolean = false

    val context = this

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_confirm_code_choise_screen)

        buttonSendCode = findViewById(R.id.button_send_confirm_code)
        buttonCheckCode = findViewById(R.id.button_confirm_acc)
        inputCode = findViewById(R.id.inputConformCode)
        radioButtonsChoise = findViewById(R.id.radiogroupchoiseConfirmAcc)

        radioButtonsChoise?.setOnCheckedChangeListener { group, checkedId ->
            when(checkedId) {
                R.id.conform_mail_btn -> {
                    emailConfirm = true
                    phoneConfirm = false
                }
                R.id.conform_phone_btn -> {
                    emailConfirm = false
                    phoneConfirm = true
                }
            }
        }
        
        buttonSendCode?.setOnClickListener {

            buttonSendCode?.isEnabled = false
            Toast.makeText(context, "Зачекайте будь-ласка..", Toast.LENGTH_SHORT).show()

            val requests = ResClientBuilder.buildClient(ResRequests::class.java)
            val response = requests.confirmationAccountSendCode(AccountConfirmation(0, Storage.mail, "", phoneConfirm, emailConfirm))

            response.enqueue(object: Callback<JsonObject> {
                override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                    if (response.isSuccessful) {

                        Toast.makeText(context, "Код підтвердження відправлений", Toast.LENGTH_SHORT).show()

                    } else {
                        when(response.code()) {
                            404 -> {
                                Toast.makeText(context, "Помилка при відправленні коду 404", Toast.LENGTH_SHORT).show()
                                buttonSendCode?.isEnabled = true
                            }
                            400 -> {
                                Toast.makeText(context, "Помилка при відправленні коду 400", Toast.LENGTH_SHORT).show()
                                buttonSendCode?.isEnabled = true
                            }
                            410 -> {
                                Toast.makeText(context, "Помилка при відправленні коду 410", Toast.LENGTH_SHORT).show()
                                buttonSendCode?.isEnabled = true
                            }
                            else -> {
                                Toast.makeText(context, "Сталася помилка при відправленні коду", Toast.LENGTH_SHORT).show()
                                buttonSendCode?.isEnabled = true
                            }
                        }
                    }
                }

                override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                    buttonSendCode?.isEnabled = true
                    Toast.makeText(context, "Сталася помилка при відправленні коду", Toast.LENGTH_SHORT).show()
                }
            })

        }

        buttonCheckCode?.setOnClickListener {

            var inputCodeText = inputCode?.text.toString()
            if(inputCodeText == "") {
                Toast.makeText(context, "Заповніть поле 'код підтверждення'", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val requests = ResClientBuilder.buildClient(ResRequests::class.java)
            buttonCheckCode?.isEnabled = false

            val response = requests.confirmationAccountCheckCode(AccountConfirmationCode(0, Storage.mail, "", phoneConfirm, emailConfirm, inputCodeText))
            response.enqueue(object: Callback<JsonObject> {
                override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                    if (response.isSuccessful) {
                        Toast.makeText(context, "Користувача підтверждено", Toast.LENGTH_SHORT).show()
                        Toast.makeText(context, "Авторизація..", Toast.LENGTH_LONG).show()

                        // AUTH GET USERS TOKENS

                        val requestsAuth = AuthClientBuilder.buildClient(AuthRequests::class.java)
                        val responseAuth = requestsAuth.login(LoginRequest(Storage.mail, Storage.pass))

                        responseAuth.enqueue(object: Callback<LoginResponce> {
                            override fun onResponse(call: Call<LoginResponce>, response: Response<LoginResponce>) {
                                if (response.isSuccessful) {
                                    Toast.makeText(context, "Авторизація успішна", Toast.LENGTH_LONG).show()

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
                                    )
                                    )

                                    responseCreateSession.enqueue(object: Callback<CreateSession> {
                                        override fun onResponse(call: Call<CreateSession>, response: Response<CreateSession>) {
                                            if (response.isSuccessful) {

                                                Toast.makeText(context, "Сесія успішно створена", Toast.LENGTH_SHORT).show()

                                                val data = response.body()!!

                                                Storage.Device_id = data.Device_id
                                                Storage.Session_Id = data.Session_Id

                                                val intent = Intent(context, ActivityApiariesScreen::class.java)
                                                startActivity(intent)  // ТОЧКА ВХОДА В ПРИЛОЖЕНИЕ В АКТИВИТИ "ПАСЕКИ"

                                            } else {
                                                when(response.code()) {
                                                    404 -> {
                                                        Toast.makeText(context, "Помилка при створенні сесії 404", Toast.LENGTH_SHORT).show()
                                                    }
                                                    400 -> {
                                                        Toast.makeText(context, "Помилка при створенні сесії 400", Toast.LENGTH_SHORT).show()
                                                    }
                                                    410 -> {
                                                        Toast.makeText(context, "Помилка при створенні сесії 410", Toast.LENGTH_SHORT).show()
                                                    }
                                                    else -> {
                                                        Toast.makeText(context, "Сталася помилка при створенні сесії", Toast.LENGTH_SHORT).show()
                                                    }
                                                }
                                            }
                                        }

                                        override fun onFailure(call: Call<CreateSession>, t: Throwable) {
                                            Toast.makeText(context, "Сталася помилка при створенні сесії2", Toast.LENGTH_SHORT).show()
                                        }
                                    })


                                    // END CREATE SESSION AND FIND OR CREATE DEVICE



                                } else {
                                    when(response.code()) {
                                        404 -> {
                                            Toast.makeText(context, "Помилка при авторизації 404", Toast.LENGTH_SHORT).show()
                                            onBackPressed()
                                        }
                                        400 -> {
                                            Toast.makeText(context, "Помилка при авторизації 400", Toast.LENGTH_SHORT).show()
                                            onBackPressed()
                                        }
                                        410 -> {
                                            Toast.makeText(context, "Помилка при авторизації 410", Toast.LENGTH_SHORT).show()
                                            val intent = Intent(context, ActivityConfirmCodeChoiseScreen::class.java)
                                            startActivity(intent)
                                        }
                                        else -> {
                                            Toast.makeText(context, "Сталася помилка при авторизації", Toast.LENGTH_SHORT).show()
                                            onBackPressed()
                                        }
                                    }
                                }
                            }

                            override fun onFailure(call: Call<LoginResponce>, t: Throwable) {
                                Toast.makeText(context, "Сталася помилка при авторизації2", Toast.LENGTH_SHORT).show()
                                onBackPressed()
                            }
                        })


                        // AUTH GET USERS TOKENS END


                    } else {
                        when(response.code()) {
                            402 -> {
                                Toast.makeText(context, "Невірний код підтвердження", Toast.LENGTH_SHORT).show()
                                buttonSendCode?.isEnabled = true
                                buttonCheckCode?.isEnabled = true
                            }
                            404 -> {
                                Toast.makeText(context, "Помилка при перевірці коду підтвердження", Toast.LENGTH_SHORT).show()
                                buttonSendCode?.isEnabled = true
                                buttonCheckCode?.isEnabled = true
                            }
                            400 -> {
                                Toast.makeText(context, "Помилка при перевірці коду підтвердження", Toast.LENGTH_SHORT).show()
                                buttonSendCode?.isEnabled = true
                                buttonCheckCode?.isEnabled = true
                            }
                            410 -> {
                                Toast.makeText(context, "Помилка при перевірці коду підтвердження", Toast.LENGTH_SHORT).show()
                                buttonSendCode?.isEnabled = true
                                buttonCheckCode?.isEnabled = true
                            }
                            else -> {
                                Toast.makeText(context, "Помилка при перевірці коду підтвердження", Toast.LENGTH_SHORT).show()
                                buttonSendCode?.isEnabled = true
                                buttonCheckCode?.isEnabled = true
                            }
                        }
                    }
                }

                override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                    buttonSendCode?.isEnabled = true
                    buttonCheckCode?.isEnabled = true
                    Toast.makeText(context, "Сталася помилка(", Toast.LENGTH_SHORT).show()
                }
            })
        }
    }
}