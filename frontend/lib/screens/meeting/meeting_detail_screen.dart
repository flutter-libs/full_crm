import 'package:flutter/material.dart';
import 'package:frontend/models/user_meeting.dart';
import 'package:frontend/services/meeting_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';


class MeetingDetailScreen extends StatelessWidget {
  final int? meetingId;
  const MeetingDetailScreen({super.key, this.meetingId});
  static const String id = "meeting_detail_screen";
  @override
  Widget build(BuildContext context) {
    final MeetingAPIService _service = MeetingAPIService();

    return Scaffold(
      appBar: CustomAppBar(title: 'CRM: Meeting Detail'),
      drawer: SideNavDrawer(),
      body: FutureBuilder<UserMeeting>(
        future: _service.getMeetingById(meetingId!),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting)
            return const Center(child: CircularProgressIndicator());
          if (snapshot.hasError)
            return Center(child: Text('Error: ${snapshot.error}'));

          final meeting = snapshot.data!;
          return Padding(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text('Meeting: ${meeting.meetingId}', style: const TextStyle(fontSize: 18)),
                const SizedBox(height: 8),
                Text('Date: ${meeting.meeting!.dateCreated}'),
                const SizedBox(height: 8),
                Text('Description: ${meeting.meeting!.description ?? "No description"}'),
              ],
            ),
          );
        },
      ),
    );
  }
}