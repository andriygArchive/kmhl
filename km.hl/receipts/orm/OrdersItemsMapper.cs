using System;
using System.Collections.Generic;
using System.Text;
using g.orm.impl;

using km.hl.orm;

namespace km.hl.receipts.orm {
    public class OrdersItemsMapper : AbstractSqlMapper, OrderItem.IOrderItemMapper {
        protected override string ConnectionKey {
            get { return OrmCommons.DATABASE_ID; }
        }

        private const String BASE_SELECT = "SELECT order_item_id, seq_location, order_id, quantity, quantity_checked, inventory_item_id, item_description, item_segment1, mfg_part_num, mfg_part_num_exp, attribute1, no_serials "
            + "FROM po_hl_order_items ";
        protected override GetQueryCallback getSelectAllCb() {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override GetQueryCallback getSelectByKeyCb(g.orm.Key key) {
            throw new Exception("The method or operation is not implemented.");
        }

        private class InsertQueryCb : GetQueryCallback {
            public string Sql {
                get { return "UPDATE po_hl_order_items "
                    + "SET order_id = ?, quantity = ?, quantity_checked = ?, inventory_item_id = ?, item_description = ?, "
                    + "    item_segment1 = ?, mfg_part_num = ?, mfg_part_num_exp = ?, attribute1 = ?, no_serials = ? "
                    + "WHERE order_item_id = ? AND seq_location = ?"; }
            }

            public void SetParams(System.Data.IDbCommand cmd, g.orm.ORMObject obj) {
                OrderItem i = (OrderItem)obj;
                
                g.DbTools.setParam(cmd, ":order_id", i.Order.Id);
                g.DbTools.setParam(cmd, ":quantity", i.Quantity);
                g.DbTools.setParam(cmd, ":qty_checked", i.QuantityChecked);
                g.DbTools.setParam(cmd, ":inv_item_id", i.InventoryItemId);
                g.DbTools.setParam(cmd, ":desct", OrmCommons.encodeText(i.Description));

                g.DbTools.setParam(cmd, ":item_segment1", OrmCommons.encodeText(i.InternalCode));
                g.DbTools.setParam(cmd, ":mfg_part_num", OrmCommons.encodeText(i.MfrCode));
                g.DbTools.setParam(cmd, ":mfg_part_num_exp", OrmCommons.encodeText(OrdersItemsMapper.listToCodesString(i.MfrExtCodes)));
                g.DbTools.setParam(cmd, ":attribute1", i.Attribute);
                g.DbTools.setParam(cmd, ":no_serial", i.NoSerials ? "Y" : "N");

                g.DbTools.setParam(cmd, ":order_item_id", i.Id);
                g.DbTools.setParam(cmd, ":seq_location", i.SqType);
            }

        }
        protected override GetQueryCallback getInsertQueryCB() {
            return new InsertQueryCb();
        }

        private static String listToCodesString(ICollection<String> i) {
            StringBuilder b = new StringBuilder();
            bool first = true;
            foreach (String j in i) {
                if (!first) b.Append("/");
                b.Append(j);
                first = false;
            }
            return b.ToString();
        }

        private class UpdateQueryCb : GetQueryCallback {
            public string Sql {
                get { return "UPDATE po_hl_order_items SET quantity_checked = ?, mfg_part_num_exp = ?, no_serials = ? "
                    + "WHERE order_item_id = ? AND seq_location = ?"; }
            }

            public void SetParams(System.Data.IDbCommand cmd, g.orm.ORMObject obj) {
                OrderItem i = (OrderItem) obj;

                g.DbTools.setParam(cmd, ":quantity_checked", i.QuantityChecked);
                g.DbTools.setParam(cmd, ":mfg_part_num_exp", OrmCommons.encodeText(OrdersItemsMapper.listToCodesString(i.MfrExtCodes)));
                g.DbTools.setParam(cmd, ":no_serials",  i.NoSerials ? "Y" : "N");

                g.DbTools.setParam(cmd, ":order_item_id", i.Id);
                g.DbTools.setParam(cmd, ":seq_location", i.SqType);
            }
        }
        protected override GetQueryCallback getUpdateQueryCB() {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override GetQueryCallback getDeleteQueryCB() {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void loadInstance(g.orm.ORMObject obj, System.Data.DataRow rs) {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override g.orm.ORMObject createInstance(g.orm.Key key, System.Data.DataRow rs) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override g.orm.Key createKey(System.Data.DataRow rs) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override g.orm.Key createKey() {
            throw new Exception("The method or operation is not implemented.");
        }

        public ICollection<OrderItem> getItemsForOrder(IntKey orderKey) {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}