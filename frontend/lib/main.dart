import 'dart:io';

import 'package:flutter/material.dart';
import 'package:frontend/screens/campaign/campaign_create_screen.dart';
import 'package:frontend/screens/campaign/campaign_detail_screen.dart';
import 'package:frontend/screens/campaign/campaign_list_screen.dart';
import 'package:frontend/screens/campaign/campaign_update_screen.dart';
import 'package:frontend/screens/contact/contact_create_screen.dart';
import 'package:frontend/screens/contact/contact_detail_screen.dart';
import 'package:frontend/screens/contact/contact_list_screen.dart';
import 'package:frontend/screens/contact/contact_update_screen.dart';
import 'package:frontend/screens/dashboard_screen.dart';
import 'package:frontend/screens/lead/lead_create_screen.dart';
import 'package:frontend/screens/lead/lead_detail_screen.dart';
import 'package:frontend/screens/lead/lead_list_screen.dart';
import 'package:frontend/screens/login_screen.dart';
import 'package:frontend/screens/meeting/add_participants_screen.dart';
import 'package:frontend/screens/meeting/meeting_create_screen.dart';
import 'package:frontend/screens/meeting/meeting_detail_screen.dart';
import 'package:frontend/screens/meeting/meeting_list_screen.dart';
import 'package:frontend/screens/register_screen.dart';
import 'package:http/io_client.dart';
import 'package:toastification/toastification.dart';
import 'package:window_manager/window_manager.dart';


void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await windowManager.ensureInitialized();
  WindowOptions windowOptions = const WindowOptions(
    center: true,
    backgroundColor: Color.fromRGBO(22, 22, 22, 1),
    skipTaskbar: false,
    titleBarStyle: TitleBarStyle.normal
  );
  windowManager.waitUntilReadyToShow(windowOptions, () async {
    await windowManager.show();
    await windowManager.focus();
    await windowManager.maximize();
  });
  runApp(CRMFrontend());
}

class CRMFrontend extends StatelessWidget {
  const CRMFrontend({super.key});

  @override
  Widget build(BuildContext context) {
    return ToastificationWrapper(
        config: ToastificationConfig(
          maxTitleLines: 2,
          maxDescriptionLines: 6,
          marginBuilder: (context, alignment) => const EdgeInsets.fromLTRB(0, 16, 0, 110),
        ),
        child: MaterialApp(
          theme: ThemeData.dark(),
          initialRoute: RegisterScreen.id,
          routes: {
            RegisterScreen.id: (context) => RegisterScreen(),
            LoginScreen.id: (context) => LoginScreen(),
            DashboardScreen.id: (context) => DashboardScreen(),
            MeetingListScreen.id: (context) => MeetingListScreen(),
            MeetingDetailScreen.id: (context) => MeetingDetailScreen(
              meetingId: ModalRoute.of(context)?.settings.arguments as int?,
            ),
            MeetingCreateScreen.id: (context) => MeetingCreateScreen(),
            AddParticipantsScreen.id: (context) => AddParticipantsScreen(
              meetingId: ModalRoute.of(context)?.settings.arguments as int?,
              allUsers: [],
            ),
            LeadListScreen.id: (context) => LeadListScreen(),
            LeadDetailScreen.id: (context) => LeadDetailScreen(
                leadId: ModalRoute.of(context)?.settings.arguments as int?
            ),
            LeadCreateScreen.id: (context) => LeadCreateScreen(),
            CampaignCreateScreen.id: (context) => CampaignCreateScreen(),
            CampaignListScreen.id: (context) => CampaignListScreen(),
            CampaignDetailScreen.id:(context) => CampaignDetailScreen(
                campaignId: ModalRoute.of(context)?.settings.arguments as int?
            ),
            CampaignUpdateScreen.id: (context) => CampaignUpdateScreen(
                campaignId: ModalRoute.of(context)?.settings.arguments as int?
            ),
            ContactListScreen.id: (context) => ContactListScreen(),
            ContactCreateScreen.id: (context) => ContactCreateScreen(),
            ContactDetailScreen.id: (context) => ContactDetailScreen(
              contactId: ModalRoute.of(context)?.settings.arguments as int?
            ),
            ContactUpdateScreen.id: (context) => ContactUpdateScreen(
                contactId: ModalRoute.of(context)?.settings.arguments as int?
            ),
          },
          debugShowCheckedModeBanner: false,
        )
    );
  }
}
