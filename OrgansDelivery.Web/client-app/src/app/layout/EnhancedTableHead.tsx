import React from "react";
import Box from "@mui/material/Box";
import TableCell from "@mui/material/TableCell";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import TableSortLabel from "@mui/material/TableSortLabel";
import Checkbox from "@mui/material/Checkbox";
import { visuallyHidden } from "@mui/utils";
import { Order } from "./EntitiesTable";

export interface HeadCell {
    disablePadding: boolean;
    id: string;
    label: string;
    numeric: boolean;
}

type Props = {
    headCells: HeadCell[];
    numSelected: number;
    onRequestSort: (
        event: React.MouseEvent<unknown>,
        index: number
    ) => void;
    onSelectAllClick: (event: React.ChangeEvent<HTMLInputElement>) => void;
    order: Order;
    orderBy: number;
    rowCount: number;
}

const EnhancedTableHead = (props: Props) => {
    const {
        onSelectAllClick, order, orderBy, numSelected, headCells, rowCount, onRequestSort,
    } = props;

    const createSortHandler = (index: number) => (event: React.MouseEvent<unknown>) => {
        onRequestSort(event, index);
    };

    return (
        <TableHead>
            <TableRow>
                <TableCell padding="checkbox">
                    <Checkbox
                        color="primary"
                        indeterminate={numSelected > 0 && numSelected < rowCount}
                        checked={rowCount > 0 && numSelected === rowCount}
                        onChange={onSelectAllClick}
                        inputProps={{
                            "aria-label": "select all desserts",
                        }} />
                </TableCell>
                {headCells.map((headCell, index) => (
                    <TableCell
                        key={headCell.id}
                        align={headCell.numeric ? "right" : "left"}
                        padding={headCell.disablePadding ? "none" : "normal"}
                        sortDirection={orderBy === index ? order : false}
                        sx={{
                            fontWeight: "bold"
                        }}
                    >
                        <TableSortLabel
                            active={orderBy === index}
                            direction={orderBy === index ? order : "asc"}
                            onClick={createSortHandler(index)}
                        >
                            {headCell.label}
                            {orderBy === index ? (
                                <Box component="span" sx={visuallyHidden}>
                                    {order === "desc"
                                        ? "sorted descending"
                                        : "sorted ascending"}
                                </Box>
                            ) : null}
                        </TableSortLabel>
                    </TableCell>
                ))}
            </TableRow>
        </TableHead>
    );
}

export default EnhancedTableHead;
