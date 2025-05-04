import 'package:flutter/material.dart';
import 'package:frontend/screens/dashboard_screen.dart';
import 'package:frontend/screens/login_screen.dart';
import 'package:frontend/screens/meeting/add_participants_screen.dart';
import 'package:frontend/screens/meeting/meeting_create_screen.dart';
import 'package:frontend/screens/meeting/meeting_detail_screen.dart';
import 'package:frontend/screens/meeting/meeting_list_screen.dart';
import 'package:frontend/screens/register_screen.dart';
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
            )
          },
          debugShowCheckedModeBanner: false,
        )
    );
  }
}
