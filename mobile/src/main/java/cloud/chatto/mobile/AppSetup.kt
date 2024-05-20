package cloud.chatto.mobile

import android.app.Activity
import android.app.Application
import com.jakewharton.threetenabp.AndroidThreeTen

fun setupActivity(activity: Activity) {
    setupApplication(activity.application)
}

fun setupApplication(app: Application) {
    AndroidThreeTen.init(app)
}
