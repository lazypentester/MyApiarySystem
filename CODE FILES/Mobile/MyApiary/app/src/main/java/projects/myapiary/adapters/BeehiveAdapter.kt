package projects.myapiary.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.ImageView
import android.widget.TextView
import androidx.core.view.isVisible
import projects.myapiary.R
import projects.myapiary.models.Apiary
import projects.myapiary.models.Beehive

class BeehiveAdapter(var mCtx: Context, private val resources: Int, private val items: List<Beehive>): ArrayAdapter<Beehive>(mCtx, resources, items){
    @SuppressLint("ViewHolder", "ResourceType")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val layoutInflater: LayoutInflater = LayoutInflater.from(mCtx)
        val view: View = layoutInflater.inflate(resources, null)

        val name_of_beehive: TextView = view.findViewById(R.id.name_of_beehive)
        val status_of_beehive: TextView = view.findViewById(R.id.status_of_beehive)

        val imageBeehive: ImageView = view.findViewById(R.id.imageBeehive)

        val imageTemperature: ImageView = view.findViewById(R.id.imageTemperature)
        val imageNoise: ImageView = view.findViewById(R.id.imageNoise)
        val imageHumidity: ImageView = view.findViewById(R.id.imageHumidity)

        val textTemperature: TextView = view.findViewById(R.id.textTemperature)
        val textNoise: TextView = view.findViewById(R.id.textNoise)
        val textHumidity: TextView = view.findViewById(R.id.textHumidity)

        val imageAlarm: ImageView = view.findViewById(R.id.imageAlarm)

        val mItem = items[position]

        name_of_beehive.text = mItem.Name

        if(mItem.SensorHumidity.toInt() == 0 && mItem.SensorNoise.toInt() == 0 && mItem.SensorTemperature.toInt() == 0) {
            status_of_beehive.text = "#Не працює"
        }
        else if(mItem.SensorHumidity.toInt() == 0 || mItem.SensorNoise.toInt() == 0 || mItem.SensorTemperature.toInt() == 0) {
            status_of_beehive.text = "#Частково працює"
        }
        else {
            status_of_beehive.text = "#Працює"
        }

        imageBeehive.setImageResource(R.drawable.beehive)

        imageTemperature.setImageResource(R.drawable.temperature)
        imageNoise.setImageResource(R.drawable.noise)
        imageHumidity.setImageResource(R.drawable.humidity)

        textTemperature.text = mItem.SensorTemperature.toString()
        textNoise.text = mItem.SensorNoise.toString()
        textHumidity.text = mItem.SensorHumidity.toString()

        if(mItem.Alarm) {
            imageAlarm.setImageResource(R.drawable.error_notif); imageAlarm.isVisible = true
        } else imageAlarm.isVisible = false

        return view
    }
}