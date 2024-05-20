package cloud.chatto.mobile.activity

import android.annotation.SuppressLint
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Edit
import androidx.compose.material.icons.filled.Menu
import androidx.compose.material.icons.filled.Search
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import cloud.chatto.mobile.components.ChatItem
import cloud.chatto.mobile.data.model.Feed
import cloud.chatto.mobile.data.model.Message
import cloud.chatto.mobile.data.model.enums.FeedType
import cloud.chatto.mobile.data.model.local.DateTime
import cloud.chatto.mobile.setupActivity
import cloud.chatto.mobile.theme.MyApplicationComposeTheme
import kotlinx.coroutines.launch
import java.util.*

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setupActivity(this)
        setContent {
            MyApplicationComposeTheme {
                Activity(chats = mockChats)
            }
        }
    }
}

@SuppressLint("UnusedMaterial3ScaffoldPaddingParameter")
@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun Activity(chats: List<Chat>) {
    val drawerState = rememberDrawerState(DrawerValue.Closed)
    val scope = rememberCoroutineScope()

    ModalNavigationDrawer(
        drawerState = drawerState,
        drawerContent = {
            Column(
                modifier = Modifier
                    .fillMaxHeight()
                    .width(280.dp)
                    .background(MaterialTheme.colorScheme.background)
                    .padding(16.dp)
            ) {
                Spacer(modifier = Modifier.height(16.dp))

                Text(
                    text = "John Doe",
                    style = MaterialTheme.typography.headlineMedium,
                    color = MaterialTheme.colorScheme.onBackground
                )

                Text(
                    text = "johndoe@example.com",
                    style = MaterialTheme.typography.bodySmall,
                    color = MaterialTheme.colorScheme.onBackground
                )

                Divider(
                    color = MaterialTheme.colorScheme.onBackground,
                    modifier = Modifier.padding(vertical = 16.dp),
                )

                Button(
                    onClick = { /* Handle settings click here */ },
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Text("Settings")
                }

                Spacer(modifier = Modifier.height(8.dp))

                Button(
                    onClick = { /* Handle logout click here */ },
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Text("Logout")
                }
            }
        },
        content = {
            Scaffold(
                topBar = {
                    TopAppBar(
                        title = { Text("ChattO - ${chats.size} chats") },
                        navigationIcon = {
                            IconButton(onClick = { scope.launch { drawerState.open() } }) {
                                Icon(Icons.Filled.Menu, contentDescription = "Menu")
                            }
                        },
                        actions = {
                            IconButton(onClick = {  }) {
                                Icon(Icons.Filled.Search, contentDescription = "Search")
                            }
                        }
                    )
                },
                content = {
                    Surface(modifier = Modifier.fillMaxSize(), color = MaterialTheme.colorScheme.background) {
                        Box(modifier = Modifier.fillMaxSize()) {
                            ChatsList(chats = chats)

                            FloatingActionButton(
                                onClick = { /* Handle new chat action here */ },
                                modifier = Modifier
                                    .align(Alignment.BottomEnd)
                                    .padding(16.dp)
                                    .size(56.dp)
                            ) {
                                Icon(Icons.Filled.Edit, contentDescription = "New Chat")
                            }
                        }
                    }
                }
            )
        }
    )
}

data class Chat(val name: String, val lastMessage: String, val timeSince: Long = 0, val uuid: UUID)

val mockChats = listOf(
    Chat("Design team", "Hello!", 10, UUID.fromString("00000000-0000-0000-0000-000000000000")),
    Chat("Alex", "How are you?", 120, UUID.fromString("00000000-0000-0000-0000-000000000001")),
    Chat("Alice", "Wassup friend?", 241, UUID.fromString("00000000-0000-0000-0000-000000000003")),
    Chat("Marketing team", "Meeting at 3 PM", 600, UUID.fromString("00000000-0000-0000-0000-000000000004")),
    Chat("John", "Did you get the report?", 1200, UUID.fromString("00000000-0000-0000-0000-000000000005")),
    Chat("Development team", "New sprint starts tomorrow", 1800, UUID.fromString("00000000-0000-0000-0000-000000000006")),
    Chat("Emily", "Lunch at 12?", 3600, UUID.fromString("00000000-0000-0000-0000-000000000007")),
    Chat("Operations team", "Quarterly review next week", 5400, UUID.fromString("00000000-0000-0000-0000-000000000008")),
    Chat("Oliver", "Can you send me the files?", 7200, UUID.fromString("00000000-0000-0000-0000-000000000009")),
    Chat("Sales team", "Client meeting rescheduled", 10800, UUID.fromString("00000000-0000-0000-0000-000000000010")),
    Chat("Charlotte", "Happy birthday!", 14400, UUID.fromString("00000000-0000-0000-0000-000000000011")),
    Chat("HR team", "Don't forget to submit your timesheets", 21600, UUID.fromString("00000000-0000-0000-0000-000000000012")),
    Chat("Liam", "How's the project going?", 28800, UUID.fromString("00000000-0000-0000-0000-000000000013")),
    Chat("Finance team", "Budget report is ready", 36000, UUID.fromString("00000000-0000-0000-0000-000000000014")),
    Chat("Sophia", "Can we talk?", 43200, UUID.fromString("00000000-0000-0000-0000-000000000015")),
    Chat("Legal team", "Contract has been reviewed", 50400, UUID.fromString("00000000-0000-0000-0000-000000000016")),
    Chat("Mason", "Thanks for the help!", 57600, UUID.fromString("00000000-0000-0000-0000-000000000017")),
    Chat("Support team", "Issue #234 has been resolved", 64800, UUID.fromString("00000000-0000-0000-0000-000000000018")),
    Chat("Amelia", "See you soon!", 72000, UUID.fromString("00000000-0000-0000-0000-000000000019")),
    Chat("Product team", "Feature release on track", 79200, UUID.fromString("00000000-0000-0000-0000-000000000020")),
    Chat("Ava", "Got it, thanks!", 86400, UUID.fromString("00000000-0000-0000-0000-000000000021")), // 1 day ago
    Chat("Logistics team", "Shipment delayed", 93600, UUID.fromString("00000000-0000-0000-0000-000000000022")),
    Chat("Elijah", "Call me when you're free", 100800, UUID.fromString("00000000-0000-0000-0000-000000000023")),
    Chat("Creative team", "Brainstorming session at 2", 108000, UUID.fromString("00000000-0000-0000-0000-000000000024")),
    Chat("Harper", "Can we meet tomorrow?", 115200, UUID.fromString("00000000-0000-0000-0000-000000000025")),
    Chat("Tech support", "Server maintenance tonight", 122400, UUID.fromString("00000000-0000-0000-0000-000000000026")),
    Chat("Evelyn", "Sounds good", 129600, UUID.fromString("00000000-0000-0000-0000-000000000027")),
    Chat("Admin team", "Office closed on Monday", 136800, UUID.fromString("00000000-0000-0000-0000-000000000028")),
    Chat("James", "I'll be there", 144000, UUID.fromString("00000000-0000-0000-0000-000000000029")),
    Chat("R&D team", "New project approved", 151200, UUID.fromString("00000000-0000-0000-0000-000000000030")),
    Chat("Ella", "Let's catch up later", 158400, UUID.fromString("00000000-0000-0000-0000-000000000031")),
    Chat("PR team", "Press release tomorrow", 165600, UUID.fromString("00000000-0000-0000-0000-000000000032")),
    Chat("Aiden", "Got your message", 172800, UUID.fromString("00000000-0000-0000-0000-000000000033")), // 2 days ago
    Chat("Planning team", "Strategy meeting on Thursday", 180000, UUID.fromString("00000000-0000-0000-0000-000000000034")),
    Chat("Grace", "I'll send it over", 187200, UUID.fromString("00000000-0000-0000-0000-000000000035")),
    Chat("Events team", "Team outing next Friday", 194400, UUID.fromString("00000000-0000-0000-0000-000000000036")),
    Chat("Lucas", "See you then", 201600, UUID.fromString("00000000-0000-0000-0000-000000000037")),
    Chat("IT team", "System update completed", 208800, UUID.fromString("00000000-0000-0000-0000-000000000038")),
    Chat("Sofia", "Got it!", 216000, UUID.fromString("00000000-0000-0000-0000-000000000039")),
    Chat("Admin team", "New policy updates", 223200, UUID.fromString("00000000-0000-0000-0000-000000000040")),
    Chat("Henry", "Thanks for the update", 230400, UUID.fromString("00000000-0000-0000-0000-000000000041")),
    Chat("Recruitment team", "Interviews tomorrow", 237600, UUID.fromString("00000000-0000-0000-0000-000000000042")),
    Chat("Isabella", "I'll be there", 244800, UUID.fromString("00000000-0000-0000-0000-000000000043")),
    Chat("Management team", "Budget approval needed", 252000, UUID.fromString("00000000-0000-0000-0000-000000000044")),
    Chat("Mia", "Okay", 259200, UUID.fromString("00000000-0000-0000-0000-000000000045")),
    Chat("QA team", "Bug report submitted", 266400, UUID.fromString("00000000-0000-0000-0000-000000000046")),
    Chat("Ethan", "Great, thanks!", 273600, UUID.fromString("00000000-0000-0000-0000-000000000047")),
    Chat("Training team", "New sessions available", 280800, UUID.fromString("00000000-0000-0000-0000-000000000048")),
    Chat("Aria", "Let's discuss this", 288000, UUID.fromString("00000000-0000-0000-0000-000000000049")),
    Chat("Executive team", "Board meeting tomorrow", 295200, UUID.fromString("00000000-0000-0000-0000-000000000050")),
    Chat("Sebastian", "I agree", 302400, UUID.fromString("00000000-0000-0000-0000-000000000051")),
    Chat("Customer service", "Ticket #456 resolved", 309600, UUID.fromString("00000000-0000-0000-0000-000000000052")),
    Chat("Zoe", "Thanks a lot", 316800, UUID.fromString("00000000-0000-0000-0000-000000000053")),
    Chat("Legal team", "Policy review complete", 324000, UUID.fromString("00000000-0000-0000-0000-000000000054")),
    Chat("Benjamin", "I'll handle it", 331200, UUID.fromString("00000000-0000-0000-0000-000000000055")),
    Chat("Operations team", "New SOPs released", 338400, UUID.fromString("00000000-0000-0000-0000-000000000056")),
    Chat("Chloe", "Noted, thanks!", 345600, UUID.fromString("00000000-0000-0000-0000-000000000057")),
    Chat("Procurement team", "Vendor contracts updated", 352800, UUID.fromString("00000000-0000-0000-0000-000000000058")),
    Chat("Logan", "I'll review it", 360000, UUID.fromString("00000000-0000-0000-0000-000000000059")),
    Chat("Strategy team", "Planning meeting on Friday", 367200, UUID.fromString("00000000-0000-0000-0000-000000000060")),
    Chat("Scarlett", "Sounds good", 374400, UUID.fromString("00000000-0000-0000-0000-000000000061")),
    Chat("Finance team", "Tax documents ready", 381600, UUID.fromString("00000000-0000-0000-0000-000000000062")),
    Chat("Levi", "Got it, thanks", 388800, UUID.fromString("00000000-0000-0000-0000-000000000063")),
    Chat("Research team", "Survey results are in", 396000, UUID.fromString("00000000-0000-0000-0000-000000000064")),
    Chat("Riley", "Good to know", 403200, UUID.fromString("00000000-0000-0000-0000-000000000065")),
    Chat("Admin team", "Building maintenance on Saturday", 410400, UUID.fromString("00000000-0000-0000-0000-000000000066")),
    Chat("Dylan", "I'll be there", 417600, UUID.fromString("00000000-0000-0000-0000-000000000067")),
    Chat("Support team", "Ticket #789 resolved", 424800, UUID.fromString("00000000-0000-0000-0000-000000000068")),
    Chat("Lily", "Thanks for the update", 432000, UUID.fromString("00000000-0000-0000-0000-000000000069")),
    Chat("Development team", "Code review at 4 PM", 439200, UUID.fromString("00000000-0000-0000-0000-000000000070")),
    Chat("Owen", "I'll check it out", 446400, UUID.fromString("00000000-0000-0000-0000-000000000071")),
    Chat("Sales team", "Monthly targets achieved", 453600, UUID.fromString("00000000-0000-0000-0000-000000000072")),
    Chat("Ella", "Good job team!", 460800, UUID.fromString("00000000-0000-0000-0000-000000000073")),
    Chat("Creative team", "New design ideas", 468000, UUID.fromString("00000000-0000-0000-0000-000000000074")),
    Chat("Daniel", "Let's discuss tomorrow", 475200, UUID.fromString("00000000-0000-0000-0000-000000000075")),
    Chat("PR team", "Media outreach update", 482400, UUID.fromString("00000000-0000-0000-0000-000000000076")),
    Chat("Hannah", "Got it, thanks", 489600, UUID.fromString("00000000-0000-0000-0000-000000000077")),
    Chat("Tech team", "System upgrade scheduled", 496800, UUID.fromString("00000000-0000-0000-0000-000000000078")),
    Chat("Sebastian", "Sounds good", 504000, UUID.fromString("00000000-0000-0000-0000-000000000079")),
    Chat("Events team", "Planning meeting on Wednesday", 511200, UUID.fromString("00000000-0000-0000-0000-000000000080")),
    Chat("Luna", "See you then", 518400, UUID.fromString("00000000-0000-0000-0000-000000000081")),
    Chat("HR team", "Policy updates released", 525600, UUID.fromString("00000000-0000-0000-0000-000000000082")), // 1 week ago
    Chat("Mateo", "Thanks for letting me know", 532800, UUID.fromString("00000000-0000-0000-0000-000000000083")),
    Chat("Logistics team", "Inventory audit tomorrow", 540000, UUID.fromString("00000000-0000-0000-0000-000000000084")),
    Chat("Grace", "I'll be there", 547200, UUID.fromString("00000000-0000-0000-0000-000000000085")),
    Chat("Planning team", "Strategy review session", 554400, UUID.fromString("00000000-0000-0000-0000-000000000086")),
    Chat("Emma", "Got it!", 561600, UUID.fromString("00000000-0000-0000-0000-000000000087")),
    Chat("Executive team", "Budget finalization tomorrow", 568800, UUID.fromString("00000000-0000-0000-0000-000000000088")),
    Chat("Olivia", "I'll prepare it", 576000, UUID.fromString("00000000-0000-0000-0000-000000000089")),
    Chat("Recruitment team", "New hires orientation", 583200, UUID.fromString("00000000-0000-0000-0000-000000000090")),
    Chat("Liam", "Welcome aboard!", 590400, UUID.fromString("00000000-0000-0000-0000-000000000091")),
    Chat("Marketing team", "Campaign results are in", 597600, UUID.fromString("00000000-0000-0000-0000-000000000092")),
    Chat("Amelia", "Great news!", 604800, UUID.fromString("00000000-0000-0000-0000-000000000093")),
    Chat("QA team", "Testing complete", 612000, UUID.fromString("00000000-0000-0000-0000-000000000094")),
    Chat("Noah", "Thanks for the update", 619200, UUID.fromString("00000000-0000-0000-0000-000000000095")),
    Chat("Support team", "Issue #789 resolved", 626400, UUID.fromString("00000000-0000-0000-0000-000000000096")),
    Chat("Mia", "Good to know", 633600, UUID.fromString("00000000-0000-0000-0000-000000000097")),
    Chat("IT team", "Backup completed", 640800, UUID.fromString("00000000-0000-0000-0000-000000000098")),
    Chat("Alexander", "Thanks, all set", 648000, UUID.fromString("00000000-0000-0000-0000-000000000099")),
)

@Composable
fun ChatsList(chats: List<Chat>) {
    LazyColumn {
        items(chats) { chat ->
            val feed = Feed(chat.uuid, chat.name, "Description", FeedType.Chat)
            val message = Message(UUID.randomUUID(), chat.lastMessage, DateTime(System.currentTimeMillis() / 1000 - chat.timeSince))
            ChatItem(feed, message)
        }
    }
}
