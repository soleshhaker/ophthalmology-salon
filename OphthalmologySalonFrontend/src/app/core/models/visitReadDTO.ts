export interface visitReadDTO {
  id: number;
  start: Date;
  end: Date;
  applicationUser: string;
  applicationUserId: string;
  visitType: string;
  visitStatus: string;
  cost: number;
  additionalInfo: string;
}
