mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  GetPlatform: function(){
    MyGameInstance.SendMessage("Yandex","SetPlatform", platform);
  },

 SaveData:function(data)
 {
  var dataString = UTF8ToString(data);
  var object = JSON.parse(dataString);
  player.setData(object);
 },

 LoadData:function()
 {
   player.getData().then(_data=>{
    const json = JSON.stringify(_data);
    MyGameInstance.SendMessage("Yandex","LoadData",json);
   });
 },

  RateGame:function(){
    ysdk.feedback.canReview()
    .then(({ value, reason }) => {
      if (value) {
        ysdk.feedback.requestReview()
        .then(({ feedbackSent }) => {
          console.log(feedbackSent);
        })
      } else {
        console.log(reason)
      }
    })
  }
});