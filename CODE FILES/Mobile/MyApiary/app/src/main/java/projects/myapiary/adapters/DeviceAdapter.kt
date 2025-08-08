package projects.myapiary.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.TextView
import projects.myapiary.R
import projects.myapiary.models.Device

class DeviceAdapter(var mCtx: Context, private val resources: Int, private val items: List<Device>): ArrayAdapter<Device>(mCtx, resources, items) {
    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val layoutInflater: LayoutInflater = LayoutInflater.from(mCtx)
        val view: View = layoutInflater.inflate(resources, null)

        val name_of_device: TextView = view.findViewById(R.id.name_of_device)
        val uuid_of_device: TextView = view.findViewById(R.id.uuid_of_device)

        val mItem = items[position]

        name_of_device.text = mItem.Name
        uuid_of_device.text = mItem.Fingerprint

        return view
    }
}