package projects.myapiary

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import projects.myapiary.api.server.auth.AuthClientBuilder
import projects.myapiary.api.server.auth.AuthRequests
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.AccountRegRequest
import projects.myapiary.models.AccountRegResponce
import projects.myapiary.models.LoginRequest
import projects.myapiary.models.LoginResponce
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.lang.reflect.Type

class ActivityRegisterScreen : AppCompatActivity() {

    private var buttonRegistration: Button? = null
    private var goToLogin: TextView? = null
    private var inputName: EditText? = null
    private var inputSurname: EditText? = null
    private var inputPhone: EditText? = null
    private var inputMail: EditText? = null
    private var inputPass: EditText? = null

    val context = this

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register_screen)

        goToLogin = findViewById(R.id.button_get_started_login)
        inputName = findViewById(R.id.inputPersonName)
        inputSurname = findViewById(R.id.inputPersonSurname)
        inputPhone = findViewById(R.id.inputRegPhone)
        inputMail = findViewById(R.id.inputRegEmailAddress)
        inputPass = findViewById(R.id.inputRegPassword)

        buttonRegistration = findViewById(R.id.button_reg)

        goToLogin?.setOnClickListener {
            val intent = Intent(context, ActivityAuthScreen::class.java)
            startActivity(intent)
        }

        buttonRegistration?.setOnClickListener {

        var userName = inputName?.text.toString()
        var userSurname = inputSurname?.text.toString()
        var userPhone = inputPhone?.text.toString()
        var userMail = inputMail?.text.toString()
        var userPass = inputPass?.text.toString()

        if(userName == "")
            Toast.makeText(context, "Поле 'Ім'я' не заповнено", Toast.LENGTH_SHORT).show()
        else if(userSurname == "")
            Toast.makeText(context, "Поле 'Прізвище' не заповнено", Toast.LENGTH_SHORT).show()
        else if(userPhone == "")
            Toast.makeText(context, "Поле 'Телефон' не заповнено", Toast.LENGTH_SHORT).show()
        else if(userMail == "")
            Toast.makeText(context, "Поле 'Поштова скринька' не заповнено", Toast.LENGTH_SHORT).show()
        else if(userPass == "")
            Toast.makeText(context, "Поле 'Пароль' не заповнено", Toast.LENGTH_SHORT).show()
            else {
                buttonRegistration?.isEnabled = false

                val requests = ResClientBuilder.buildClient(ResRequests::class.java)
                val response = requests.registration(AccountRegRequest(userName, userSurname, userPhone, userMail, userPass))

            response.enqueue(object: Callback<AccountRegResponce> {
                override fun onResponse(call: Call<AccountRegResponce>, response: Response<AccountRegResponce>) {
                        if (response.isSuccessful) {
                            Toast.makeText(context, "Реєстрація успішна", Toast.LENGTH_SHORT).show()
                            Toast.makeText(context, "Авторизація..", Toast.LENGTH_SHORT).show()
                            val data = response.body()!!

                            Storage.id = data.id
                            Storage.name = userName
                            Storage.surname = userSurname
                            Storage.phone = userPhone
                            Storage.mail = userMail
                            Storage.pass = userPass


                            //AUTH GET USER TOKENS


                            val requestsAuthFromReg = AuthClientBuilder.buildClient(AuthRequests::class.java)
                            val responseAuthFromReg = requestsAuthFromReg.login(LoginRequest(Storage.mail, Storage.pass))

                            responseAuthFromReg.enqueue(object: Callback<LoginResponce> {
                                override fun onResponse(call: Call<LoginResponce>, response: Response<LoginResponce>) {
                                    if (response.isSuccessful) {

                                        Toast.makeText(context, "Треба підтвердити аккаунт", Toast.LENGTH_SHORT).show()
                                        val intent = Intent(context, ActivityConfirmCodeChoiseScreen::class.java)
                                        startActivity(intent)

                                    } else {
                                        when(response.code()) {
                                            404 -> {
                                                Toast.makeText(context, "Користувача з таким логіном або паролем не існує", Toast.LENGTH_SHORT).show()
                                                val intent = Intent(context, ActivityAuthScreen::class.java)
                                                startActivity(intent)
                                            }
                                            400 -> {
                                                Toast.makeText(context, "Невірний формат пошти", Toast.LENGTH_SHORT).show()
                                                val intent = Intent(context, ActivityAuthScreen::class.java)
                                                startActivity(intent)
                                            }
                                            410 -> {
                                                Toast.makeText(context, "Треба підтвердити аккаунт", Toast.LENGTH_SHORT).show()
                                                val intent = Intent(context, ActivityConfirmCodeChoiseScreen::class.java)
                                                startActivity(intent)
                                            }
                                            else -> {
                                                Toast.makeText(context, "Сталася помилка(", Toast.LENGTH_SHORT).show()
                                                val intent = Intent(context, ActivityAuthScreen::class.java)
                                                startActivity(intent)
                                            }
                                        }
                                    }
                                }

                                override fun onFailure(call: Call<LoginResponce>, t: Throwable) {
                                    Toast.makeText(context, "Сталася помилка(", Toast.LENGTH_SHORT).show()
                                    val intent = Intent(context, ActivityAuthScreen::class.java)
                                    startActivity(intent)
                                }
                            })


                            // AUTH GET USER TOKENS END


                        } else {
                            when(response.code()) {
                                411 -> {
                                    Toast.makeText(context, "Користувач із такою електронною адресою вже існує", Toast.LENGTH_SHORT).show()
                                    buttonRegistration?.isEnabled = true
                                }
                                412 -> {
                                    Toast.makeText(context, "Користувач із таким номером телефону вже існує", Toast.LENGTH_SHORT).show()
                                    buttonRegistration?.isEnabled = true
                                }
                                else -> {
                                    Toast.makeText(context, "Сталася помилка(", Toast.LENGTH_SHORT).show()
                                    buttonRegistration?.isEnabled = true
                                }
                            }
                        }
                    }

                    override fun onFailure(call: Call<AccountRegResponce>, t: Throwable) {
                        Toast.makeText(context, "Помилка при реєстрації", Toast.LENGTH_SHORT).show()
                        buttonRegistration?.isEnabled = true
                    }
                })
            }

        }
    }
}