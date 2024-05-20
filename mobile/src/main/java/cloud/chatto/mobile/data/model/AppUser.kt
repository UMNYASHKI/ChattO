package cloud.chatto.mobile.data.model

import java.util.UUID

data class AppUser(
    val id: UUID,
    val displayName: String,
    val email: String? = null,
)