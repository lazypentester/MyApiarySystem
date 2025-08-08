package projects.myapiary

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button

class ActivityHelpScreen : AppCompatActivity() {

    private var buttonGoBackFromhelp: Button? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_help_screen)

        buttonGoBackFromhelp = findViewById(R.id.buttonGoBackFromHelp)

        buttonGoBackFromhelp?.setOnClickListener {
            onBackPressed();
        }
    }
}