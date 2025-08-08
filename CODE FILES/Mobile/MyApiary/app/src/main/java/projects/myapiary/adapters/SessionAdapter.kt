package projects.myapiary.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.TextView
import com.google.android.material.timepicker.TimeFormat
import projects.myapiary.R
import projects.myapiary.models.Session
import java.sql.Time
import java.text.DateFormat
import java.text.SimpleDateFormat
import java.time.format.DateTimeFormatter
import java.util.*

class SessionAdapter(var mCtx: Context, private val resources: Int, private val items: List<Session>): ArrayAdapter<Session>(mCtx, resources, items) {
    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val layoutInflater: LayoutInflater = LayoutInflater.from(mCtx)
        val view: View = layoutInflater.inflate(resources, null)

        val ip_address: TextView = view.findViewById(R.id.ip_address)
        val date: TextView = view.findViewById(R.id.date)
        val geolocation: TextView = view.findViewById(R.id.geolocation)

        val mItem = items[position]

        ip_address.text = mItem.Ip_address

        try {

            val jud = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.ms").parse(mItem.Start_date)
            //val month: String = DateFormat.getDateInstance(SimpleDateFormat.LONG, Locale("ru")).format(jud)
            val time15: String = DateFormat.getDateTimeInstance(DateFormat.SHORT, DateFormat.SHORT, Locale.forLanguageTag("ru")).format(jud)

            //val datetime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.ms", Locale("ru")).parse(mItem.Start_date)
            //date.text = datetime?.toString()
            date.text = time15
        } catch (e: Exception) {
            date.text = "---"
        }

        geolocation.text = mItem.Geolocation

        return view
    }
}