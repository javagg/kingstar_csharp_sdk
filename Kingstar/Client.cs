using System;
using System.Net.Sockets;

namespace Kingstar
{
    public class Client
    {
        public String UserId { get; set; }
        public String Password { get; private set; }
        public String AgentId { get; set; }

        private string ip;
        private int port;

        private TcpClient socket = null;
        private NetworkStream stream = null;

        public delegate void OnConnected();
        public delegate void OnDisconnected();
        public delegate void OnError();
        public delegate void OnUserLogin();
        public delegate void OnUserLogout();
        public delegate void OnHeartBeatWarning();

        public delegate void OnSubMarketData();
        public delegate void OnUnSubMarketData();
        public delegate void OnMarketData();


        public Client(string ip, int port, string userId, string password, string agentId)
        {
            this.UserId = userId;
            this.Password = password;
            this.AgentId = agentId;
            this.ip = ip;
            this.port = port;
        }

        public void Connect()
        {
            this.socket = new TcpClient(this.ip, this.port);
            this.stream = socket.GetStream();
            SendLogin();

        }

        public void UserLogin()
        {
        }

        public void UserLogout()
        {
        }

        public void Disconnect()
        {
        }

        private void SendLogin()
        {
            var stream = this.socket.GetStream();
            if (this.socket.Connected && stream.CanWrite)
            {
            }
        }

        private void SendLogout()
        {
        }

        public void QuerySettlementInfo(){
        }

        public void QuerySettlementInfoConfirm(){
        }

        public void UpdateUserPassword() {
        }

        public void UpdateTradingAccountPassword() {
        }

        public void QueryBrokerTradingParams() {
        }

        public void QueryBrokerTradingAlgos() {
        }

        public void QueryInstrument() {
        }

        public void QueryTradingAccount() {
        }

        public void QueryTradingCode() {
        }

        public void QueryInvestorOpenPosition() {
        }

        public void QueryInvestorOpenCombinePosition() {
        }

        public void QueryInvestorPosition() {
        }

        public void QueryOrder() {
        }

        public void QueryTrade() {
        }

        public void QueryInvestor() {
        }

        public void QueryExchange() {
        }

        public void QueryInstrumentCommissionRate() {
        }

        public void QueryDepthMarketData() {
        }

        public void QueryNotice() {
        }

        public void QueryInvestorPositionCombineDetail() {
        }

        public void QueryMaxOrderVolume() {
        }

        public void QueryEWarrantOffset() {
        }

        public void QueryCFMMCTradingAccountKey() {
        }

        public void QueryTransferJournal() {
        }

        public void QueryTradingNotice() {
        }

        public void ConfirmSettlementInfo() {
        }

        public void QueryAccountRegistration() {
        }

        public void QueryContractBank() {
        }

        public void InsertQrder() {
        }

        public void QueryInstrumentMarginRate() {
        }
            

        public void QueryInvestorPositionDetail() {
        }

        public void BatchCancelOrder() {
        }

        public void InitConditionalOrderInsertion() {
        }

        public void QueryConditionalOrder() {
        }

        public void InsertStopOrder() {
        }

        public void ModifyConditionalOrder() {
        }

        public void ModifyStopOrder() {
        }

        public void SuspendOrActivateConditionalOrder() {
        }

        public void QueryStopOrder() {
        }

        public void RemoveConditionalOrder() {
        }

        public void RemoveStopOrder() {
        }

        public void SelectConditionalOrder() {
        }

        public void SelectStopOrder() {
        }
    }
}

