package projects.myapiary

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.navigation.findNavController
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController
import com.google.android.material.bottomnavigation.BottomNavigationView

class ActivityApiariesScreen : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_apiaries_screen)

        val bottomNavigationView = findViewById<BottomNavigationView>(R.id.bottomNavigationView)

        //val navController = findNavController(R.id.fragmentContainerView)

        val navHostFragment = supportFragmentManager.findFragmentById(R.id.fragmentContainerView) as NavHostFragment
        val navController = navHostFragment.navController

        // code for action bar
        //val appBarConfiguration = AppBarConfiguration(setOf(R.id.firstFragment, R.id.secondFragment))
        //setupActionBarWithNavController(navController, appBarConfiguration)

        bottomNavigationView.setupWithNavController(navController)
    }
}