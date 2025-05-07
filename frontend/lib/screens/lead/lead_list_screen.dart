import 'package:flutter/material.dart';
import 'package:frontend/models/lead.dart';
import 'package:frontend/screens/lead/lead_detail_screen.dart';
import 'package:frontend/services/lead_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';

class LeadListScreen extends StatefulWidget {
  const LeadListScreen({super.key});

  static const String id = "lead_list_screen";

  @override
  State<LeadListScreen> createState() => _LeadListScreenState();
}

class _LeadListScreenState extends State<LeadListScreen> {
  late Future<List<Lead>> _leadsFuture;

  @override
  void initState() {
    super.initState();
    _leadsFuture = LeadApiService().getAllLeads();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: CustomAppBar(title: 'CRM: Lead List'),
      drawer: SideNavDrawer(),
      body: FutureBuilder(
        future: _leadsFuture,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }
          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }
          final leads = snapshot.data ?? [];
          if (leads.isEmpty) {
            return const Center(child: Text('No leads found'));
          }
          return ListView.builder(
            itemCount: leads.length,
            itemBuilder: (context, index) {
              final lead = leads[index];
              return Card(
                margin: const EdgeInsets.symmetric(
                  horizontal: 12.0,
                  vertical: 6.0,
                ),
                child: ListTile(
                  title: Text(lead.leadName ?? 'Unnamed Lead'),
                  subtitle: Text(lead.leadEmail ?? 'No Email'),
                  trailing: Column(
                    children: [
                      Row(
                        children: [
                          Text(lead.leadCity ?? ''),
                          Text(', '),
                          Text(lead.leadState ?? ''),
                          Text(', '),
                          Text(lead.leadZip ?? ''),
                        ],
                      ),
                      const SizedBox(height: 30),
                      Text(
                        lead.createdByUser?.userName ?? '',
                        style: TextStyle(
                          color: Colors.indigoAccent,
                          fontFamily: 'Ubuntu',
                          fontWeight: FontWeight.w700,
                        ),
                      ),
                    ],
                  ),
                  onTap: () {
                    Navigator.pushNamed(context, LeadDetailScreen.id);
                  },
                ),
              );
            },
          );
        },
      ),
    );
  }
}
