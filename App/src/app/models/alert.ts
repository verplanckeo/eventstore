export class Alert{
    id: string;
    type: AlertType;
    message: string;
    autoClose: boolean;
    keepAfterRouteChange: boolean;
    fade: boolean;

    constructor(){
        // Question: Why use this instead of standard CTor?
        // constructor(init?:Partial<Alert>){
        //     Object.assign(this, init);
        // }
     }

     public static CreateEmptyAlert(id: string){
         let alert = new Alert();
         alert.id = id;
         return alert;
     }
     
     public static CreateAlert(type: AlertType, message: string, keepAfterRouteChange: boolean, fade: boolean): Alert{
         let alert = new Alert();
         alert.type = type;
         alert.message = message;
         alert.keepAfterRouteChange = keepAfterRouteChange;
         alert.fade = fade;
         alert.autoClose = true;

         return alert;
     }

}


export enum AlertType{
    Error = -1,
    Success = 0,
    Info = 1,
    Warning = 2
}