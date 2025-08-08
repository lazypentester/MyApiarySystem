package projects.myapiary.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.graphics.drawable.Drawable
import android.os.Build
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import projects.myapiary.ActivityBeehivesScreen
import projects.myapiary.ActivityNotificationScreen
import projects.myapiary.FirstFragment
import projects.myapiary.R
import projects.myapiary.models.Apiary

class ApiaryAdapter(var mCtx: Context, private val resources: Int, private val items: List<Apiary>): ArrayAdapter<Apiary>(mCtx, resources, items){
    @SuppressLint("ViewHolder", "ResourceType")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {

        val layoutInflater: LayoutInflater = LayoutInflater.from(mCtx)
        val view: View = layoutInflater.inflate(resources, null)

        val name_of_apiary: TextView = view.findViewById(R.id.name_of_apiary)
        val address_of_apiary: TextView = view.findViewById(R.id.address_of_apiary)

        val imageApiaries: ImageView = view.findViewById(R.id.imageApiaries)

        val imageConnectionWire: ImageView = view.findViewById(R.id.imageConnectionWire)
        val imageConnectionWireless: ImageView = view.findViewById(R.id.imageConnectionWireless)
        val imageCountOfBeehives: ImageView = view.findViewById(R.id.imageCountOfBeehives)
        val textCountOfBeehives: TextView = view.findViewById(R.id.textCountOfBeehives)
        val imageNotifications: ImageView = view.findViewById(R.id.imageNotifications)

        imageNotifications.setOnClickListener {
            var intent = Intent (mCtx, ActivityNotificationScreen::class.java)
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
                intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            }
            intent.putExtra("apiary_id", items[position].id)
            mCtx.startActivity(intent)
        }

        val mItem = items[position]

        name_of_apiary.text = mItem.Name
        address_of_apiary.text = mItem.Address

        imageApiaries.setImageResource(R.drawable.apiary01)

        if(!mItem.ConnectionWire) imageConnectionWire.setImageResource(R.drawable.no_internet) else imageConnectionWire.setImageResource(R.drawable.connect_apiary_true)
        if(!mItem.ConnectionWireless) imageConnectionWireless.setImageResource(R.drawable.connect_beehives_false) else imageConnectionWireless.setImageResource(R.drawable.connect_beehives_true)

        imageCountOfBeehives.setImageResource(R.drawable.beehives_count)
        textCountOfBeehives.text = mItem.CountOfBeehives.toString()

        if(!mItem.Notifications) imageNotifications.setImageResource(R.drawable.notification_svg_false) else imageNotifications.setImageResource(R.drawable.notification_svg_true)

        return view
    }
}