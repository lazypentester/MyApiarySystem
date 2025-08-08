package projects.myapiary

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.TextView

class ActivityNotificationSinglePage : AppCompatActivity() {

    private var buttonGoBackFromNotification: Button? = null
    private var buttonOkFromNotif: Button? = null

    private var textNotificationTitle: TextView? = null
    private var textNotificationDate: TextView? = null
    private var textNotificationDescription: TextView? = null


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_notification_single_page)

        buttonGoBackFromNotification = findViewById(R.id.buttonGoBackFromNotification)
        buttonOkFromNotif = findViewById(R.id.buttonOkFromNotif)

        textNotificationTitle = findViewById(R.id.textNotificationTitle)
        textNotificationDate = findViewById(R.id.textNotificationDate)
        textNotificationDescription = findViewById(R.id.textNotificationDescription)

        buttonGoBackFromNotification?.setOnClickListener {
            onBackPressed()
        }

        buttonOkFromNotif?.setOnClickListener {
            onBackPressed()
        }

        textNotificationTitle?.text = intent.extras!!.getString("name")
        textNotificationDate?.text = intent.extras!!.getString("date")
        textNotificationDescription?.text = intent.extras!!.getString("desc")


    }
}