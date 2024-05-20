package cloud.chatto.mobile.data.model

import cloud.chatto.mobile.data.model.enums.FeedType
import java.util.UUID

data class Feed(
    val id: UUID,
    val name: String,
    val description: String,
    val feedType: FeedType
)