package cloud.chatto.mobile.data.model

import cloud.chatto.mobile.data.model.local.DateTime
import java.util.UUID

data class Message(
    val id: UUID,
    val text: String,
    val createdAt: DateTime,
)