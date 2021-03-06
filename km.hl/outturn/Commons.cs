﻿using System;
using System.Collections.Generic;
using System.Text;

using km.hl.orm;
using km.hl.outturn.orm;

namespace km.hl.outturn {
    class Commons {
        public static bool checkSerialExists(String serial) {
            IItemsSerialsMapper mapper = (IItemsSerialsMapper)OrmContext.Instance.getMapper(typeof(ItemSerial));
            foreach (ItemSerial s in mapper.getSerialsForSerial(serial)) {
                if (s.ORMState != g.orm.StateType.DELETED) {
                    return true;
                }
            }
            return false;
        }

        public static bool checkSerialIsItemCode(String serial) {
            IMoveOrderItemsMapper mapper = (IMoveOrderItemsMapper)OrmContext.Instance.getMapper(typeof(MoveOrderItem));
            foreach (MoveOrderItem orders in mapper.getItemsForInternalCode(serial)) {
                return true;
            }
            foreach (MoveOrderItem orders in mapper.getItemsForMfrCode(serial)) {
                return true;
            }
            return false;
        }
    }
}
