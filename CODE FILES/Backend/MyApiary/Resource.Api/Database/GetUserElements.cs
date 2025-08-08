using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Resource.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Database
{
    public class GetUserElements
    {
        public static List<ESensor> GetUserSensors(int user_id, ProjectContext context)
        {
            List<ESensor> Sensors;

            string query = "Select distinct([Sensors].[Id]), Min_value, Max_value, Is_working, BeehiveId, Value From T_sensors, Enterprises, Rooms" +
                    $" Where [Enterprises].[UserId] = @user_id and" +
                    $" [Rooms].[EnterpriseId] = [Enterprises].[Id] and" +
                    $" [T_sensors].[RoomId] = [Rooms].[Id]";

            return Sensors = context.Sensors.FromSqlRaw(query, new SqlParameter("user_id", user_id)).ToList();
        }

        public static List<ESensor> GetUserSensorsFromBeehive(int beehive_id, ProjectContext context)
        {
            List<ESensor> Sensors;

            string query = "Select * From Sensors Where [Sensors].[BeehiveId] = @beehive_id";

            return Sensors = context.Sensors.FromSqlRaw(query, new SqlParameter("beehive_id", beehive_id)).ToList();
        }


        public static List<EBeehive> GetUserBeehives(int user_id, ProjectContext context)
        {
            List<EBeehive> beehives;

            string query = "Select distinct([Beehives].[Id]), [Beehives].[Name], ApiaryId, Alarm From Apiaries, Beehives" +
                    $" Where [Apiaries].[UserId] = @user_id and" +
                    $" [Beehives].[ApiaryId] = [Apiaries].[Id]";

            return beehives = context.Beehives.FromSqlRaw(query, new SqlParameter("user_id", user_id)).ToList();
        }

        public static List<ENotification> GetUserNotifications(int user_id, ProjectContext context)
        {
            List<ENotification> notifications;

            string query = "Select distinct([Notifications].[Id]), [Notifications].[Name], [Notifications].[Description], [Notifications].[Created_date], " +
                "[Notifications].[Read_date], [Notifications].[Readed] From Notifications" +
                    $" Where [Notifications].[UserId] = @user_id";

            return notifications = context.Notifications.FromSqlRaw(query, new SqlParameter("user_id", user_id)).ToList();
        }

        public static List<ESession> GetUserSessions(int device_id, ProjectContext context)
        {
            List<ESession> sessions;

            string query = "Select * From Sessions Where [Sessions].[DeviceId] = @device_id";

            return sessions = context.Sessions.FromSqlRaw(query, new SqlParameter("device_id", device_id)).ToList();
        }

        public static List<ERrt_blacklist> GetUserRrt_blacklists(int user_id, ProjectContext context)
        {
            List<ERrt_blacklist> rrt_Blacklists;

            string query = "Select distinct([Rrt_blacklist].[Id]), [Rrt_blacklist].[Refresh_token] From Rrt_blacklist, Devices" +
                    $" Where [Devices].[UserId] = @user_id and" +
                    $" [Rrt_blacklist].[DeviceId] = [Devices].[Id]";

            return rrt_Blacklists = context.Rrt_Blacklists.FromSqlRaw(query, new SqlParameter("user_id", user_id)).ToList();
        }

        public static List<EDevice> GetUserDevices(int user_id, ProjectContext context)
        {
            List<EDevice> devices;

            string query = "Select * From Devices" +
                    $" Where [Devices].[UserId] = @user_id";

            return devices = context.Devices.FromSqlRaw(query, new SqlParameter("user_id", user_id)).ToList();
        }

        public static List<EConfirmation> GetUserConfirmations(int user_id, ProjectContext context)
        {
            List<EConfirmation> confirmations;

            string query = "Select distinct([Confirmations].[Id]), [Confirmations].[Secret_code], [Confirmations].[Activated] From Confirmations" +
                    $" Where [Confirmations].[UserId] = @user_id";

            return confirmations = context.Confirmations.FromSqlRaw(query, new SqlParameter("user_id", user_id)).ToList();
        }

        public static List<EApiary> GetUserApiaries(int user_id, ProjectContext context)
        {
            List<EApiary> apiaries;

            string query = "Select * From Apiaries Where [Apiaries].[UserId] = @user_id";

            return apiaries = context.Apiaries.FromSqlRaw(query, new SqlParameter("user_id", user_id)).ToList();
        }

        public static bool AddNewItem(int user_id, string item, ProjectContext context)
        {
            bool add = false;

            ETariff user_tariff = context.Tariffs.Find(context.Users.Find(user_id).TariffId);

            int user_apiaries = GetUserApiaries(user_id, context).Count();
            int user_beehives = GetUserBeehives(user_id, context).Count();

            switch (item.ToLower())
            {
                case "apiary":
                    if (user_apiaries < user_tariff.Max_apiaries)
                        add = true;
                    break;
                case "beehive":
                    if (user_beehives < user_tariff.Max_beehives)
                        add = true;
                    break;
            }

            return add;
        }

        public static void SetAlarm(int beehive_id, ProjectContext context)
        {
            string query = "Update Beehives Set Alarm = true Where Id = @beehive_id";

            context.Apiaries.FromSqlRaw(query, new SqlParameter("beehive_id", beehive_id));
        }
    }
}
