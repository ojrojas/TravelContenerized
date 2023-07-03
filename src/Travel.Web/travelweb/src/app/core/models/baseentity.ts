export interface IBaseEntity {
    id: string;
    modifiedBy?: string;
    modifiedOn?: Date | string;
    createdBy: string;
    createdOn: Date | string;
    status: boolean;
}