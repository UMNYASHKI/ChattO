package cloud.chatto.mobile.components

import android.os.Build
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.text.style.TextOverflow
import androidx.compose.ui.unit.dp
import cloud.chatto.mobile.data.model.Feed
import cloud.chatto.mobile.data.model.Message
import cloud.chatto.mobile.data.model.local.DateTime
import org.threeten.bp.LocalDateTime
import org.threeten.bp.ZoneOffset
import org.threeten.bp.format.DateTimeFormatter
import org.threeten.bp.format.TextStyle
import org.threeten.bp.temporal.ChronoUnit
import java.util.Locale

@Composable
fun ChatItem(feed: Feed, lastMessage: Message) {
    Row(
        modifier = Modifier
            .fillMaxWidth()
            .clickable { /* Handle chat item click here */ }
            .padding(8.dp)
    ) {
        NoAvatar(feed) // Replace with actual avatar color or image
        Spacer(modifier = Modifier.width(8.dp))
        Column(modifier = Modifier.weight(1f).padding(top = 1.dp)) {
            Text(text = feed.name, style = MaterialTheme.typography.bodyLarge, fontWeight = FontWeight.Medium)
            Text(
                text = lastMessage.text,
                style = MaterialTheme.typography.bodyMedium,
                maxLines = 1,
                overflow = TextOverflow.Ellipsis
            )
        }
        Text(
            text = resolveMessageTime(lastMessage.createdAt),
            style = MaterialTheme.typography.bodyMedium,
            textAlign = TextAlign.End,
            modifier = Modifier.padding(start = 8.dp)
        )
    }
}

fun resolveMessageTime(time: DateTime): String {
    val now = LocalDateTime.now()
    val messageTime = LocalDateTime.ofEpochSecond(time.value, 0, ZoneOffset.UTC)

    return when {
        messageTime.truncatedTo(ChronoUnit.DAYS).isEqual(now.truncatedTo(ChronoUnit.DAYS)) -> {
            messageTime.format(DateTimeFormatter.ofPattern("HH:mm"))
        }
        messageTime.truncatedTo(ChronoUnit.DAYS).isEqual(now.minusDays(1).truncatedTo(ChronoUnit.DAYS)) -> {
            "Yesterday ${messageTime.format(DateTimeFormatter.ofPattern("HH:mm"))}"
        }
        messageTime.isAfter(now.minusDays(7)) -> {
            messageTime.dayOfWeek.getDisplayName(TextStyle.FULL, Locale.getDefault())
        }
        else -> {
            messageTime.format(DateTimeFormatter.ofPattern("MMM dd"))
        }
    }
}