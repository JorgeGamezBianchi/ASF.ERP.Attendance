using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASF.ERP
{
    //public class NotifyActionFilter : ActionFilterAttribute
    //{
    //    static List<KeyValuePair<string, ENotificationTypes>> list = new List<KeyValuePair<string, ENotificationTypes>>();
    //    static List<KeyValuePair<ENotificationTypes, ENotificationTypes>> listSecondaryCheck = new List<KeyValuePair<ENotificationTypes, ENotificationTypes>>();
    //    public NotifyActionFilter()
    //    {
    //        if(list.Count == 0)
    //        {
    //            // GASTOS
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesCheckingDetails", ENotificationTypes.ExpCheckingPartialRepaidNotitication));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesCheckingEdit", ENotificationTypes.ExpCheckingAuthorizationCorrectionRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesCheckingEdit", ENotificationTypes.ExpCheckingRepaidCorrectionRequestToCreator));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesReviewEdit", ENotificationTypes.ExpCheckingAuthorizationRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesReviewEdit", ENotificationTypes.ExpCheckingRepaidCorrectionRequestToAuth));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesRepaymentEdit", ENotificationTypes.ExpCheckingRepaidRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesCheckingClose", ENotificationTypes.ExpCheckingRepaidNotiticationToCreator));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesCheckingClose", ENotificationTypes.ExpCheckingRepaidNotiticationToAuth));

    //            listSecondaryCheck.Add(new KeyValuePair<ENotificationTypes, ENotificationTypes>(ENotificationTypes.ExpCheckingRepaidNotiticationToCreator, ENotificationTypes.ExpCheckingRepaidNotiticationToAuth));
    //            listSecondaryCheck.Add(new KeyValuePair<ENotificationTypes, ENotificationTypes>(ENotificationTypes.ExpCheckingRepaidNotiticationToAuth, ENotificationTypes.ExpCheckingRepaidNotiticationToCreator));
    //            listSecondaryCheck.Add(new KeyValuePair<ENotificationTypes, ENotificationTypes>(ENotificationTypes.ExpCheckingRepaidCorrectionRequestToCreator, ENotificationTypes.ExpCheckingRepaidCorrectionRequestToAuth));
    //            listSecondaryCheck.Add(new KeyValuePair<ENotificationTypes, ENotificationTypes>(ENotificationTypes.ExpCheckingRepaidCorrectionRequestToAuth, ENotificationTypes.ExpCheckingRepaidCorrectionRequestToCreator));

    //            // VIATICOS
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesCheckingDetails", ENotificationTypes.ViaticsPartialDeliveryNotitication));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesCheckingEdit", ENotificationTypes.ViaticsAuthorizationCorrectionRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesCheckingEdit", ENotificationTypes.ViaticsDeliveryCorrectionRequestToCreator));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesReviewEdit", ENotificationTypes.ViaticsAuthorizationRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesReviewEdit", ENotificationTypes.ViaticsDeliveryCorrectionRequestToAuth));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesRepaymentEdit", ENotificationTypes.ViaticsDeliveryRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesCheckingClose", ENotificationTypes.ViaticsDeliveryNotiticationToCreator));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("ExpensesCheckingClose", ENotificationTypes.ViaticsDeliveryNotiticationToAuth));

    //            listSecondaryCheck.Add(new KeyValuePair<ENotificationTypes, ENotificationTypes>(ENotificationTypes.ViaticsDeliveryNotiticationToCreator, ENotificationTypes.ViaticsDeliveryNotiticationToAuth));
    //            listSecondaryCheck.Add(new KeyValuePair<ENotificationTypes, ENotificationTypes>(ENotificationTypes.ViaticsDeliveryNotiticationToAuth, ENotificationTypes.ViaticsDeliveryNotiticationToCreator));
    //            listSecondaryCheck.Add(new KeyValuePair<ENotificationTypes, ENotificationTypes>(ENotificationTypes.ViaticsDeliveryCorrectionRequestToCreator, ENotificationTypes.ViaticsDeliveryCorrectionRequestToAuth));
    //            listSecondaryCheck.Add(new KeyValuePair<ENotificationTypes, ENotificationTypes>(ENotificationTypes.ViaticsDeliveryCorrectionRequestToAuth, ENotificationTypes.ViaticsDeliveryCorrectionRequestToCreator));

    //            // PRESUPUESTOS
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("BudgetUpdateEdit", ENotificationTypes.BudgetsRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("BudgetDetails", ENotificationTypes.BudgetsRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("BudgetUpdateEdit", ENotificationTypes.BudgetsRequestReissue));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("BudgetDetails", ENotificationTypes.BudgetsRequestReissue));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("BudgetUpdateEdit", ENotificationTypes.BudgetsRequestEditedInUpdating));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("BudgetDetails", ENotificationTypes.BudgetsRequestEditedInUpdating));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("BudgetRequestCloseOrReissue", ENotificationTypes.BudgetsPartiallyUpdated));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("BudgetDetails", ENotificationTypes.BudgetsPartiallyUpdated));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("BudgetRequestCloseOrReissue", ENotificationTypes.BudgetsUpdated));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("BudgetDetails", ENotificationTypes.BudgetsUpdated));

    //            // SEGURIDAD
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorDetails", ENotificationTypes.SafetyBehaviorRegistered));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorValidationEdit", ENotificationTypes.SafetyBehaviorRegistered));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorDetails", ENotificationTypes.SafetyBehaviorReviewed));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorRegisterEdit", ENotificationTypes.SafetyBehaviorReviewed));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorDetails", ENotificationTypes.SafetyBehaviorAttended));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorValidationClose", ENotificationTypes.SafetyBehaviorAttended));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorDetails", ENotificationTypes.SafetyBehaviorRejected));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorRegisterEdit", ENotificationTypes.SafetyBehaviorRejected));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorDetails", ENotificationTypes.SafetyBehaviorClosed));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorDetails", ENotificationTypes.SafetyBehaviorExpired));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorRegisterEdit", ENotificationTypes.SafetyBehaviorExpired));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorValidationEdit", ENotificationTypes.SafetyBehaviorExpired));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("SafetyBehaviorValidationClose", ENotificationTypes.SafetyBehaviorExpired));


    //            // REQUISICIONES
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionAuthorizationEdit", ENotificationTypes.RequisitionAuthorizationRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionDetails", ENotificationTypes.RequisitionAuthorizedToCreator));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionValidationEdit", ENotificationTypes.RequisitionValidationRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionAttendEdit", ENotificationTypes.RequisitionAttendRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionDetails", ENotificationTypes.RequisitionReissuedToCreator));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionDetails", ENotificationTypes.RequisitionReissuedToAuthorizer));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionDetails", ENotificationTypes.RequisitionReissuedToAttendant));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionDetails", ENotificationTypes.RequisitionRejected));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionReviewEdit", ENotificationTypes.RequisitionReviewRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionDetails", ENotificationTypes.RequisitionAttentionProcess));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionClose", ENotificationTypes.RequisitionProcessing));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionClose", ENotificationTypes.RequisitionCompleted));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionDetails", ENotificationTypes.RequisitionClosed));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionDetails", ENotificationTypes.RequisitionOpened));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionAttendEdit", ENotificationTypes.RequisionTemporalProductsUpdated));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("RequisitionDetails", ENotificationTypes.RequisitionCancelled));

    //            // ORDENES DE COMPRA
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("PurchaseOrderRequestEdit", ENotificationTypes.PurchaseOrderAttentionRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("PurchaseOrderDetails", ENotificationTypes.PurchaseOrderClosed));

    //            // ESTIMACIONES
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("EstimateDetails", ENotificationTypes.EstimateAttentionRequest));
    //            list.Add(new KeyValuePair<string, ENotificationTypes>("EstimateAttendEdit", ENotificationTypes.EstimateAttentionRequest));
    //        }
    //    }

    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        //Obtiene el nombre de la acción
    //        var actionName = filterContext.ActionDescriptor.ActionName;
    //        if (actionName == null)
    //            return;

    //        //Obtiene la lista de notificaciones para check de acuerdo a la acción
    //        var filterList = list.Where(s => s.Key.ToLower() == actionName.ToLower());
    //        if (filterList.Count() == 0)
    //            return;

    //        //Intenta obtener el id para actualizar la notificación
    //        if (filterContext.ActionParameters.TryGetValue("id", out object idValue))
    //        {
    //            if (idValue != null)
    //            {
    //                NotificationsBL notificationsBL = new NotificationsBL();

    //                //Actualiza las notificaciones de acuerdo al usuario actual, el tipo de notificación y el evento o id
    //                AdministrationBL adminBL = new AdministrationBL();
    //                Users usr = adminBL.UsersSearch(HttpContext.Current.User.Identity.GetUserName());

    //                foreach (KeyValuePair<string, ENotificationTypes> notification in filterList)
    //                {
    //                    //Obtiene un rol de acuerdo a un controlador y acción para refrescar las notificaciones por rol
    //                    int RolTargetId = 0;
    //                    GeneralBL generalBL = new GeneralBL();
    //                    SystemParameters roleTargetPar;
    //                    switch (notification.Value)
    //                    {
    //                        case ENotificationTypes.ExpCheckingRepaidRequest:
    //                            roleTargetPar = generalBL.GetSysParameter("Gastos-RolReembolso");
    //                            if(roleTargetPar != null)
    //                            {
    //                                int RolTargetIdAux = 0;
    //                                if (int.TryParse(roleTargetPar.ParameterValue, out RolTargetIdAux))
    //                                    RolTargetId = RolTargetIdAux;
    //                            }
    //                            break;
    //                        case ENotificationTypes.ViaticsDeliveryRequest:
    //                            roleTargetPar = generalBL.GetSysParameter("Viaticos-RolEntrega");
    //                            if (roleTargetPar != null)
    //                            {
    //                                int RolTargetIdAux = 0;
    //                                if (int.TryParse(roleTargetPar.ParameterValue, out RolTargetIdAux))
    //                                    RolTargetId = RolTargetIdAux;
    //                            }
    //                            break;
    //                        case ENotificationTypes.BudgetsRequest:
    //                            roleTargetPar = generalBL.GetSysParameter("Presupuestos-RolActualizacion");
    //                            if (roleTargetPar != null)
    //                            {
    //                                int RolTargetIdAux = 0;
    //                                if (int.TryParse(roleTargetPar.ParameterValue, out RolTargetIdAux))
    //                                    RolTargetId = RolTargetIdAux;
    //                            }
    //                            break;
    //                        case ENotificationTypes.BudgetsRequestReissue:
    //                            roleTargetPar = generalBL.GetSysParameter("Presupuestos-RolActualizacion");
    //                            if (roleTargetPar != null)
    //                            {
    //                                int RolTargetIdAux = 0;
    //                                if (int.TryParse(roleTargetPar.ParameterValue, out RolTargetIdAux))
    //                                    RolTargetId = RolTargetIdAux;
    //                            }
    //                            break;
    //                        case ENotificationTypes.RequisitionAttendRequest:
    //                            roleTargetPar = generalBL.GetSysParameter("Requisiciones-RolAtencion");
    //                            if (roleTargetPar != null)
    //                            {
    //                                int RolTargetIdAux = 0;
    //                                if (int.TryParse(roleTargetPar.ParameterValue, out RolTargetIdAux))
    //                                    RolTargetId = RolTargetIdAux;
    //                            }
    //                            break;
    //                        //case ENotificationTypes.RequisitionValidationRequest:
    //                        //    roleTargetPar = generalBL.GetSysParameter("Requisiciones-RolValidacion");
    //                        //    if (roleTargetPar != null)
    //                        //    {
    //                        //        int RolTargetIdAux = 0;
    //                        //        if (int.TryParse(roleTargetPar.ParameterValue, out RolTargetIdAux))
    //                        //            RolTargetId = RolTargetIdAux;
    //                        //    }
    //                        //    break;
    //                        case ENotificationTypes.PurchaseOrderAttentionRequest:
    //                            roleTargetPar = generalBL.GetSysParameter("OrdenesCompra-RolActualizacion");
    //                            if (roleTargetPar != null)
    //                            {
    //                                int RolTargetIdAux = 0;
    //                                if (int.TryParse(roleTargetPar.ParameterValue, out RolTargetIdAux))
    //                                    RolTargetId = RolTargetIdAux;
    //                            }
    //                            break;
    //                        case ENotificationTypes.EstimateAttentionRequest:
    //                            roleTargetPar = generalBL.GetSysParameter("Estimaciones-RolAtencion");
    //                            if (roleTargetPar != null)
    //                            {
    //                                int RolTargetIdAux = 0;
    //                                if (int.TryParse(roleTargetPar.ParameterValue, out RolTargetIdAux))
    //                                    RolTargetId = RolTargetIdAux;
    //                            }
    //                            break;
    //                        default:
    //                            break;
    //                    }


    //                    long convertedId = 0;
    //                    if (idValue is int)
    //                        convertedId = (long)(int)idValue;
    //                    if (idValue is long)
    //                        convertedId = (long)idValue;

    //                    notificationsBL.CheckNotification(usr.UserId, RolTargetId, (short)notification.Value, convertedId);

    //                    var filterListSecondary = listSecondaryCheck.Where(s => s.Key == notification.Value);
    //                    if(filterListSecondary != null)
    //                        foreach (KeyValuePair<ENotificationTypes, ENotificationTypes> notificationSecondary in filterListSecondary)
    //                        {
    //                            notificationsBL.CheckSecondaryNotification((short)notificationSecondary.Value, convertedId);
    //                        }
    //                }
    //            }
    //        }

    //        //var id = filterContext.RouteData.Values["id"];
    //    }
    //}
}