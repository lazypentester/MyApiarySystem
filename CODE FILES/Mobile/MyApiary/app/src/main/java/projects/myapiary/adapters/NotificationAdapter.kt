package projects.myapiary.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import projects.myapiary.R
import projects.myapiary.models.Apiary
import projects.myapiary.models.NotificationsByApiary
import java.text.DateFormat
import java.text.SimpleDateFormat
import java.util.*

class NotificationAdapter(var mCtx: Context, private val resources: Int, private val items: List<NotificationsByApiary>): ArrayAdapter<NotificationsByApiary>(mCtx, resources, items) {

    @SuppressLint("ViewHolder", "ResourceType")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {

        val layoutInflater: LayoutInflater = LayoutInflater.from(mCtx)
        val view: View = layoutInflater.inflate(resources, null)


        val NotifName: TextView = view.findViewById(R.id.NotifName)
        val NotifDate: TextView = view.findViewById(R.id.NotifDate)

        val imageNotificationIs: ImageView = view.findViewById(R.id.imageNotificationIs)
        val notificationGetRead: ImageView = view.findViewById(R.id.notificationGetRead)

        val mItem = items[position]

        NotifName.text = mItem.Name

        try {

/*            val jud = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.ms").parse(mItem.Created_date)
            //val month: String = DateFormat.getDateInstance(SimpleDateFormat.LONG, Locale("ru")).format(jud)
            val time15: String = DateFormat.getDateTimeInstance(DateFormat.SHORT, DateFormat.SHORT, Locale.forLanguageTag("ru")).format(jud)

            //val datetime = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.ms", Locale("ru")).parse(mItem.Start_date)
            //date.text = datetime?.toString()*/
            //NotifDate.text = datetime?.toString()
        } catch (e: Exception) {
            //NotifDate.text = "---"
        }
        NotifDate.text = mItem.Created_date

        notificationGetRead.setImageResource(R.drawable.right_arrov_svg_notif)

        if(mItem.Readed) imageNotificationIs.setImageResource(R.drawable.email_notif_false_svg) else imageNotificationIs.setImageResource(
            R.drawable.email_notif_true_svg)


        return view
    }

}