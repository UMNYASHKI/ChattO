package cloud.chatto.mobile.components

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import cloud.chatto.mobile.data.model.AppUser
import cloud.chatto.mobile.data.model.Feed
import java.util.UUID

@Composable
fun NoAvatar(feed: Feed) {
    val (backgroundColor, textColor) = getAvatarColorAndTextColor(feed.id)
    Box(
        modifier = Modifier
            .size(48.dp)
            .clip(CircleShape)
            .background(backgroundColor)
    ) {
        if (feed.name.isNotBlank()) {
            Text(
                text = feed.name.first().toString(),
                color = textColor,
                style = MaterialTheme.typography.bodyLarge,
                modifier = Modifier.align(Alignment.Center)
            )
        }
    }
}

private fun getAvatarColorAndTextColor(userId: UUID): Pair<Color, Color> {
    val colors = listOf(
        Color(0xFFE57373),
        Color(0xFF81C784),
        Color(0xFF64B5F6),
        Color(0xFFFFD54F),
        Color(0xFF9575CD),
        Color(0xFFFF8A65),
        Color(0xFF4DB6AC),
        Color(0xFF7986CB),
        Color(0xFFA1887F),
        Color(0xFF90A4AE)
    )
    val index = userId.hashCode() % colors.size
    return colors[index] to Color.Black
}
