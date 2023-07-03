import { ReactNode } from "react";

export interface MenuCategory {
    category:string;
    menus: MenuItem[];
}

export interface MenuItem {
    name: string;
    route:string;
    icon: ReactNode;
    subMenus?: MenuItem[];
}